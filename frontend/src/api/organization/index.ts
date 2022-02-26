import { defHttp } from '/@/utils/http/axios';
import {
  GetOrgizationTreeModel,
  GetOrgizationUserModel,
  GetOrgizationModel,
  UpdateOrgizationModel,
  CreateOrgizationModel,
} from './model/organizationModel';
import { BasicFetchResult } from '/@/api/model/baseModel';
enum Api {
  GetOrganizationTree = '/organization/tree',
  Organization = '/organization',
  GetAllocationOrganizationRoles = 'organization/role/allocation/list',
  GetAllocationOrganizationPositions = 'organization/position/allocation/list',
}

export const getOrganizationTree = () => {
  return defHttp.get<GetOrgizationTreeModel[]>({ url: Api.GetOrganizationTree });
};

export const getOrganizationUserPageList = (requestParams) => {
  return defHttp.get<BasicFetchResult<GetOrgizationUserModel>>({
    url: `/organization/${requestParams.id}/user/page`,
    params: {
      pageIndex: requestParams.pageIndex,
      pageSize: requestParams.pageSize,
    },
  });
};

export const getOrganizationById = (id) => {
  return defHttp.get<GetOrgizationModel>({
    url: `/organization/${id}`,
  });
};

export const updateOrganization = (params: UpdateOrgizationModel) => {
  return defHttp.put({
    url: Api.Organization,
    params: params,
  });
};

export const createOrganization = (params: CreateOrgizationModel) => {
  return defHttp.post({
    url: Api.Organization,
    params: params,
  });
};

export const deleteOrganization = (id: number) => {
  return defHttp.delete({
    url: `/organization/${id}`,
  });
};

export const getOrganizationUserIds = (id: number) => {
  return defHttp.get({
    url: `/organization/${id}/userids`,
  });
};

export const addOrganizationUsers = (requestParams) => {
  return defHttp.put({
    url: `/organization/${requestParams.organizationUserId}/users`,
    params: requestParams.addUsers,
  });
};

export const removeOrganizationUsers = (organizationUserId: number, userIds: number[]) => {
  return defHttp.delete({
    url: `/organization/${organizationUserId}/users`,
    params: userIds,
  });
};

export const getAllocationOrganizationRoles = () => {
  return defHttp.get({
    url: Api.GetAllocationOrganizationRoles,
  });
};

export const setAllocationOrganizationRoles = (organizationId: number, roleIds: number[]) => {
  return defHttp.put({
    url: `/organization/${organizationId}/role`,
    params: roleIds,
  });
};

export const getOrganizationPositions = () => {
  return defHttp.get({
    url: Api.GetAllocationOrganizationPositions,
  });
};

export const setAllocationOrganizationPositions = (
  organizationId: number,
  positionIds: number[],
) => {
  return defHttp.put({
    url: `/organization/${organizationId}/position`,
    params: positionIds,
  });
};
