import { defHttp } from '/@/utils/http/axios';
import { BasicFetchResult } from '/@/api/model/baseModel';
import { GetAuditLogPageModel, GetAuditLogDetailModel } from './model/auditLogModel';

enum Api {
  GetAuditLogPageList = '/auditlog/page',
}

export const getAuditLogPageList = (requestParams) => {
  return defHttp.get<BasicFetchResult<GetAuditLogPageModel>>({
    url: Api.GetAuditLogPageList,
    params: requestParams,
  });
};

export const getAuditLogDetail = (id) => {
  return defHttp.get<GetAuditLogDetailModel>({
    url: `/auditlog/${id}`,
  });
};
