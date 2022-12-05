import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'BOOKSTore',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44352/',
    redirectUri: baseUrl,
    clientId: 'BOOKSTore_App',
    responseType: 'code',
    scope: 'offline_access BOOKSTore',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44352',
      rootNamespace: 'BOOKSTore',
    },
  },
} as Environment;
