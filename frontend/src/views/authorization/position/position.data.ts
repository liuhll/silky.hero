import { omit } from 'lodash-es';
import { getPositionList } from '/@/api/position';
import { Status } from '/@/utils/status';
import { OptionsItem } from '/@/utils/model';

export const getPositionOptions = async (query) => {
  const positionList = await getPositionList(query);
  const positionOptions = positionList.reduce((prev, next: Recordable) => {
    if (next) {
      prev.push({
        ...omit(next, ['name', 'id']),
        label: next['name'],
        value: next['id'],
        disabled: next['status'] === Status.Invalid,
      });
    }
    return prev;
  }, [] as OptionsItem[]);
  return positionOptions;
};
