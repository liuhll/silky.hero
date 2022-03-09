import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { statusOptions } from '/@/utils/status';
import { Status } from '/@/utils/status';
import { checkDictionary } from '/@/api/dictionary';
import { omit } from 'lodash-es';
import { OptionsItem } from '/@/utils/model';
import { Tag } from 'ant-design-vue';
import { h } from 'vue';
import { formatToDate } from '/@/utils/dateUtil';
import { DataRange } from '/@/utils/dataRangeUtil';
import { DescItem } from '/@/components/Description/index';
import { commonTagRender } from '/@/utils/tagUtil';
import { Rule } from '/@/components/Form';

export enum DictionaryNameType {
  Name = 0,
  RealName = 1,
}

export const columns: BasicColumn[] = [
  {
    title: '字典名称',
    dataIndex: 'name',
    width: 100,
    align: 'left',
  },
  {
    title: '唯一编码',
    dataIndex: 'code',
    width: 120,
    align: 'left',
  },
  {
    title: '状态',
    dataIndex: 'status',
    width: 80,
    align: 'left',
    customRender: ({ record }) => {
      const enable = record.status === Status.Valid;
      const color = enable ? 'green' : 'red';
      const text = enable ? '正常' : '冻结';
      return h(Tag, { color: color }, () => text);
    },
  },
  {
    title: '排序',
    dataIndex: 'sort',
    width: 50,
    align: 'left',
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
    label: '标识',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'realName',
    label: '名称',
    component: 'Input',
    colProps: { span: 6 },
  },
];

export const dictionarySchemas: FormSchema[] = [
  {
    field: 'name',
    component: 'Input',
    label: '字典标识',
    helpMessage: '英文字符和数字组合,不允许超过50个字符',
    colProps: {
      span: 12,
    },
  },
  {
    field: 'realName',
    component: 'Input',
    label: '字典名称',
    colProps: {
      span: 12,
    },
  },
  {
    field: 'sort',
    component: 'InputNumber',
    componentProps: {
      style: { width: '100%' },
    },
    label: '排序',
    colProps: {
      span: 12,
    },
  },
  {
    field: 'isDefault',
    label: '默认',
    component: 'RadioButtonGroup',
    componentProps: {
      options: [
        { label: '否', value: false },
        { label: '是', value: true },
      ],
    },
    defaultValue: false,
    colProps: {
      span: 12,
    },
  },
  {
    field: 'isPublic',
    label: '公共',
    component: 'RadioButtonGroup',
    componentProps: {
      options: [
        { label: '否', value: false },
        { label: '是', value: true },
      ],
    },
    defaultValue: false,
    colProps: {
      span: 12,
    },
  },
  {
    field: 'isStatic',
    label: '静态',
    component: 'RadioButtonGroup',
    componentProps: {
      options: [
        { label: '否', value: false },
        { label: '是', value: true },
      ],
    },
    defaultValue: false,
    colProps: {
      span: 12,
    },
  },
  {
    field: 'status',
    label: '状态',
    component: 'RadioButtonGroup',
    componentProps: {
      options: statusOptions,
    },
    defaultValue: Status.Valid,
    colProps: {
      span: 12,
    },
  },
  {
    field: 'remark',
    component: 'InputTextArea',
    label: '备注',
  },
];

const checkDictionaryRule = async (value: string, id: Nullable<number>, dictionaryNameType: DictionaryNameType) => {
  if (value) {
    const exist = await checkDictionary({
      id: id,
      name: value,
      dictionaryNameType: dictionaryNameType,
    });
    if (exist) {
      return Promise.reject(`已经存在${value}的字典`);
    }
  }
  return Promise.resolve();
};

export const getNameRules = (id: Nullable<number>): Rule[] => {
  return [
    {
      required: true,
      message: '字典不允许为空',
    },
    {
      max: 50,
      message: '字典标识长度不允许超过50个字符',
      validateTrigger: ['change', 'blur'],
    },
    {
      type: 'string',
      pattern: new RegExp('^\\w+$'),
      message: '字典格式不正确',
      validateTrigger: ['change', 'blur'],
    },
    {
      type: 'string',
      validateTrigger: ['change', 'blur'],
      validator: (rules, value) => {
        return checkDictionaryRule(value, id, DictionaryNameType.Name);
      },
    },
  ];
};

export const getRealNameRules = (id: Nullable<number>): Rule[] => {
  return [
    {
      required: true,
      message: '字典名称不允许为空',
    },
    {
      max: 50,
      message: '字典名称长度不允许超过50个字符',
      validateTrigger: ['change', 'blur'],
    },
    {
      type: 'string',
      validateTrigger: ['change', 'blur'],
      validator: (rules, value) => {
        return checkDictionaryRule(value, id, DictionaryNameType.RealName);
      },
    },
  ];
};

export const dictionaryDataSchemas: FormSchema[] = [
  {
    field: 'dataRange',
    label: '数据范围',
    component: 'Select',
    required: true,
    slot: 'dataRangeSlot',
  },
  {
    field: 'customOrganizationIds',
    label: '自定义部门',
    component: 'TreeSelect',
    componentProps: {
      checkable: true,
      multiple: true,
      defaultExpandAll: true,
    },
    rules: [
      {
        validator: (rule, value) => {
          return new Promise((resolve, reject) => {
            if (value.length <= 0) {
              reject();
            } else {
              resolve();
            }
          });
        },
        message: '请选择字典有的数据权限部门',
      },
    ],
    ifShow: ({ values }) => {
      return values.dataRange === DataRange.CustomOrganization.valueOf();
    },
  },
];

export const getDictionaryOptions = async (query: any) => {
  const dictionaryList = await getDictionaryList(query);
  const dictionaryOptions = dictionaryList.reduce((prev, next: Recordable) => {
    if (next) {
      prev.push({
        ...omit(next, ['realName', 'id']),
        label: next['realName'],
        value: next['id'],
        disabled: next['status'] === Status.Invalid,
      });
    }
    return prev;
  }, [] as OptionsItem[]);
  return dictionaryOptions;
};

export const getUserDictionaryOptions = async (userId: number) => {
  const dictionaryList = await getUserDictionaryList(userId);
  const dictionaryOptions = dictionaryList.reduce((prev, next: Recordable) => {
    if (next) {
      prev.push({
        ...omit(next),
        label: next['realName'],
        value: next['name'],
        disabled: next['status'] === Status.Invalid,
      });
    }
    return prev;
  }, [] as OptionsItem[]);
  return dictionaryOptions;
};

export const dictionaryDetailSchemas: DescItem[] = [
  {
    label: '标识',
    field: 'name',
  },
  {
    label: '字典名',
    field: 'realName',
  },
  {
    label: '是否默认',
    field: 'isDefault',
    render: (value) => {
      if (value === true) {
        return commonTagRender('blue', '是');
      } else {
        return commonTagRender('red', '否');
      }
    },
  },
  {
    label: '是否公共',
    field: 'isPublic',
    render: (value) => {
      if (value === true) {
        return commonTagRender('blue', '是');
      } else {
        return commonTagRender('red', '否');
      }
    },
  },
  {
    label: '是否静态',
    field: 'isStatic',
    render: (value) => {
      if (value === true) {
        return commonTagRender('blue', '是');
      } else {
        return commonTagRender('red', '否');
      }
    },
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
    label: '备注',
    field: 'remark',
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
