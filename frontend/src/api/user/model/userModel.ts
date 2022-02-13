export interface GetUserPageModel {
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

export interface GetUserPositionModel {
  id: number;
  name: string;
}

export interface GetUserModel {
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
  isActive: boolean;
  lockoutEnabled: boolean;
  createdTime: Date;
  updatedTime: Date;
  userSubsidiaries: UserSubsidiaryModel[];
  roles: UserRoleModel[];
}

export interface UserRoleModel {
  roleId: number;
  name: string;
  realName: string;
  status: number;
}

export interface UserSubsidiaryModel {
  positionId: number;
  positionName: string;
  organizationId: number;
  organizationName: string;
}
