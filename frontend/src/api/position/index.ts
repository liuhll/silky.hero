import { defHttp } from '/@/utils/http/axios';
import { BasicFetchResult } from '/@/api/model/baseModel';

import { GetPositionModel } from './model/positionModel';

enum Api {
  GetPositionList = '/position/list',
}


export const getPositionList = (query) => {
  return defHttp.get<GetPositionModel[]>({ url: Api.GetPositionList, params: { naem: query.name } });
}
