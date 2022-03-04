import { Status } from '/@/utils/status';
import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { DescItem } from '../../../components/Description/src/typing';
import { commonTagRender } from '/@/utils/tagUtil';
import { formatToDate } from '/@/utils/dateUtil';
import { checkEdition, getEditionList } from '/@/api/edition';
import { omit } from 'lodash-es';
import { OptionsItem } from '/@/utils/model';
import { Rule } from '/@/components/Form';

export const columns: BasicColumn[] = [
  {
    title: '名称',
    dataIndex: 'name',
    width: 120,
  },
  {
    title: '价格',
    dataIndex: 'price',
    width: 120,
    format: (value) => `¥ ${value}`.replace(/\\B(?=(\\d{3})+(?!\\d))/g, ','),
  },
  {
    title: '排序',
    dataIndex: 'sort',
    width: 50,
  },
  {
    title: '备注',
    dataIndex: 'remark',
    width: 120,
    align: 'left',
  },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: '名称',
    component: 'Input',
    colProps: { span: 6 },
  },
];

export const editionSchemas = [
  {
    field: 'name',
    component: 'Input',
    label: '名称',
    colProps: { span: 12 },
  },
  {
    field: 'price',
    component: 'InputNumber',
    label: '价格',
    componentProps: {
      style: 'width: 100%;',
      step: '0.01',
      formatter: (value) => `¥ ${value}`.replace(/\\B(?=(\\d{3})+(?!\\d))/g, ','),
      parser: (value) => value.replace(/\\$\\s?|(,*)/g, ''),
    },
    colProps: { span: 12 },
  },
  {
    field: 'sort',
    component: 'InputNumber',
    label: '排序',
    componentProps: {
      style: 'width: 100%;',
    },
    colProps: { span: 12 },
  },
  {
    field: 'remark',
    component: 'InputTextArea',
    label: '备注',
    colProps: { span: 24 },
  },
];

export const editionDetailSchemas: DescItem[] = [
  {
    field: 'name',
    label: '名称',
  },
  {
    field: 'sort',
    label: '排序',
  },
  {
    label: '状态',
    field: 'status',
    render: (value) => {
      if (value === Status.Valid) {
        return commonTagRender('green', '正常');
      } else {
        return commonTagRender('red', '冻结');
      }
    },
  },
  {
    field: 'remark',
    label: '备注',
    span: 2,
  },
  {
    label: '创建时间',
    field: 'createdTime',
    render: (value) => {
      if (value) {
        return formatToDate(value, 'YYYY-MM-DD HH:MM:ss');
      }
      return null;
    },
  },
  {
    label: '最后更新时间',
    field: 'updatedTime',
    render: (value) => {
      if (value) {
        return formatToDate(value, 'YYYY-MM-DD HH:MM:ss');
      }
      return null;
    },
  },
];

export const getEditionOptions = async () => {
  const editionList = await getEditionList();
  const editionOptions = editionList.reduce((prev, next: Recordable) => {
    if (next) {
      prev.push({
        ...omit(next, ['name', 'id']),
        label: next['name'],
        value: next['id'],
      });
    }
    return prev;
  }, [] as OptionsItem[]);
  return editionOptions;
};

const checkEditionRule = async (value: string, id: Nullable<number>) => {
  if (value) {
    const exist = await checkEdition({
      id: id,
      name: value,
    });
    if (exist) {
      return Promise.reject(`已经存在${value}的版本`);
    }
  }
  return Promise.resolve();
};

export const getNameRules = (id: Nullable<number>): Rule[] => {
  return [
    {
      required: true,
      message: '版本名称不允许为空',
    },
    {
      max: 50,
      message: '版本长度不允许超过50个字符',
      validateTrigger: ['change', 'blur'],
    },
    {
      type: 'string',
      validateTrigger: ['change', 'blur'],
      validator: (rules, value) => {
        return checkEditionRule(value, id);
      },
    },
  ];
};
