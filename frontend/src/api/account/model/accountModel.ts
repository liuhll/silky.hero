export interface LoginParams {
  account: string;
  password: string;
  tenantId: number | undefined;
}

export interface GetCurrentUserinfo {
  id: number;
  userName: string;
  realName: string;
  surname: string;
  birthDay: Date;
  sex: number;
  email: string;
  telPhone: string;
  mobilePhone: string;
}
