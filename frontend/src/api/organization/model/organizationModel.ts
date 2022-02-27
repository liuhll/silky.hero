export interface GetOrgizationTreeModel {
  id: number;
  parentId: number;
  name: string;
  code: string;
  remark: string;
  status: number;
  isBelong: boolean;
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

export interface GetOrgizationModel {
  organizationRoles: any;
  organizationPositions: any;
  id: number;
  parentId: number;
  name: string;
  sort: number;
  remark: string;
  status: number;
  isBelong: boolean;
}

export interface OrgizationRoleModel {
  roleId: number;
  name: string;
  realName: string;
  status: number;
  isDefault: boolean;
  isPublic: boolean;
  isStatic: boolean;
}

export interface UpdateOrgizationModel {
  id: number;
  parentId: number;
  name: string;
  sort: number;
  remark: string;
  status: number;
}

export interface CreateOrgizationModel {
  parentId: number;
  name: string;
  sort: number;
  remark: string;
  status: number;
}
