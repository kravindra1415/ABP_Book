using BOOKSTore.Email;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using MailKit.Net.Smtp;


namespace BOOKSTore.Register
{
    public class CustomerInfoService : BOOKSToreAppService, ICustomerInfoServices
    {
        private readonly IRepository<CustomerInfo, Guid> _repository;
        private readonly EmailSettings _emailSettings;

        public CustomerInfoService(IRepository<CustomerInfo, Guid> repository, IOptions<EmailSettings> options)
        {
            _repository = repository;
            _emailSettings = options.Value;
        }

        public async Task<PagedResultDto<CustomerInfoDto>> GetCustomers(GetCustomerDto input)
        {
            var customer = await _repository.GetListAsync();

            var totalCount = input.Filter == null
            ? await _repository.CountAsync()
            : await _repository.CountAsync(a => a.CustomerName.Contains(input.Filter));

            //var customerDto = ObjectMapper.Map<CustomerInfoDto, CustomerInfo>();

            return new PagedResultDto<CustomerInfoDto>(
                totalCount,
         ObjectMapper.Map<List<CustomerInfo>, List<CustomerInfoDto>>(customer));
        }

        public async Task<CustomerInfoDto> CreateAsync(CreateUpdateCustomerDto input)
        {
            var customer = new CustomerInfo
            {
                CustomerName = input.CustomerName,
                CustomerEmail = input.CustomerEmail,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                CourseName = input.CourseName
            };

            await _repository.InsertAsync(customer);

            return ObjectMapper.Map<CustomerInfo, CustomerInfoDto>(customer);
        }

        public bool SendEmail(EmailData emailData)
        {
            try
            {
                MimeMessage emailMessage = new();

                MailboxAddress emailFrom = new(_emailSettings.Name, _emailSettings.EmailId);
                emailMessage.From.Add(emailFrom);

                MailboxAddress emailTo = new(emailData.EmailToName, emailData.EmailToId);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = emailData.EmailSubject;

                BodyBuilder emailBodyBuilder = new()
                {
                    TextBody = emailData.EmailBody
                };
                emailMessage.Body = emailBodyBuilder.ToMessageBody();


                SmtpClient emailClient = new();

                emailClient.Connect(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSSL);
                emailClient.Authenticate(_emailSettings.EmailId, _emailSettings.Password);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                //Log Exception Details
                return false;
            }
        }
    }
}
