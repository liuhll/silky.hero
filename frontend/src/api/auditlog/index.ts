import { defHttp } from '/@/utils/http/axios';
import { BasicFetchResult } from '/@/api/model/baseModel';
import { GetAuditLogPageModel } from './model/auditLogModel';

enum Api {
  GetAuditLogPageList = '/auditlog/page',
}

export const getAuditLogPageList = (requestParams) => {
  return defHttp.get<BasicFetchResult<GetAuditLogPageModel>>({
    url: Api.GetAuditLogPageList,
    params: requestParams,
  });
};
