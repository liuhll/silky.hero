import { defHttp } from '/@/utils/http/axios';
import { BasicFetchResult } from '/@/api/model/baseModel';

import { GetPositionModel } from './model/positionModel';

enum Api {
  GetPositionList = '/position/list',
}


export const getPositionList = (query: object) => {
  return defHttp.get<GetPositionModel[]>({ url: Api.GetPositionList, params: { name: query.name } });
}
