export interface LoginParams {
  account: string;
  password: string;
  tenantName: string | undefined;
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
  tenantId: number;
  roles: string[];
}
