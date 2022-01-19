export interface GetOrgizationTreeModel {
  id: number;
  parentId: number;
  name: string;
  code: string;
  remark: string;
  status: number;
  children: GetOrgizationTreeModel[] | undefined;
}

export interface GetOrgizationUserModel {
  id: number;
  userName: string;
  realName: string;
  surname: string;
  birthDay: Date;
  sex: number;
  email: string;
  telPhone: string;
  mobilePhone: string;
  jobNumber: string;
  createdTime: Date;
}
