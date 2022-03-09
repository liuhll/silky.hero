import { defHttp } from '/@/utils/http/axios';

import { BasicFetchResult } from '../model/baseModel';
import { GetDictionaryPageModel } from './model/dictionaryModel';

enum Api {
  GetDictionaryPage = '/dictionary/type/page',
  Dictionary = '/dictionary/type',
}

export const getDictionaryPageList = (requestParams: any) => {
  return defHttp.get<BasicFetchResult<GetDictionaryPageModel>>({
    url: Api.GetDictionaryPage,
    params: requestParams,
  });
};

export const createDictionary = (requestParams: any) => {
  return defHttp.post({ url: Api.Dictionary, params: requestParams });
};

export const updateDictionary = (requestParams: any) => {
  return defHttp.put({ url: Api.Dictionary, params: requestParams });
};

export const deleteDictionary = (id: number) => {
  return defHttp.delete({ url: `/dictionary/${id}` });
};

export const getDictionaryById = (id: number) => {
  return defHttp.get({ url: `/dictionary/${id}` });
};

export const checkDictionary = (code:string) =>{
    return false;
};
