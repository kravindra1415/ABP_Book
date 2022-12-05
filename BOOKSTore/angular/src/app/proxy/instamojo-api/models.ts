
export interface Instamojo {
}

export interface PaymentOrderDetailsResponse {
  id?: string;
  transaction_id?: string;
  status?: string;
  currency?: string;
  amount?: number;
  name?: string;
  email?: string;
  phone?: string;
  description?: string;
  redirect_url?: string;
  payments: object;
  created_at?: string;
  resource_uri?: string;
}
