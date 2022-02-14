export interface GetRolePageModel {
  name: string;
  realName: string;
  sort: number;
  isDefault: boolean;
  isPublic: boolean;
}

export interface GetRoleDetailModel {
  name: string;
  realName: string;
  status: number;
  sort: number;
  remark: string;
  isDefault: boolean;
  isPublic: boolean;
  id: number;
  menus: any[];
  dataRange: number;
  customOrganizationDataRanges: any[];
}
