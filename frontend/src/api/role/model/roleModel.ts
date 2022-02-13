export interface GetRolePageModel {
  name: string;
  realName: string;
  sort: number;
  isDefault: boolean;
  isPublic: boolean;
}

export interface GetRoleModel {
  name: string;
  realName: string;
  status: number;
  sort: number;
  remark: string;
  isDefault: boolean;
  isPublic: boolean;
  id: number;
  menuIds: number[];
  dataRange: number;
  customOrganizationIds: number[];
}
