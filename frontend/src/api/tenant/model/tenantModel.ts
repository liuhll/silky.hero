export interface GetTenantModel {
  id: number;
  name: string;
  realName: string;
  editionName: string;
  status: number;
  remark: string;
}

export interface GetTenantPageModel {
  name: string;
  realName: string;
  status: number;
  remark: string;
  id: number;
}
