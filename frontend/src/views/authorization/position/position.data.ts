import { omit } from 'lodash-es';
import { getPositionList, getOrganizationPositionList, checkPosition } from '/@/api/position';
import { statusOptions } from '/@/utils/status';
import { Status } from '/@/utils/status';
import { Tag } from 'ant-design-vue';
import { h } from 'vue';
import { OptionsItem } from '/@/utils/model';
import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { DescItem } from '../../../components/Description/src/typing';
import { commonTagRender } from '/@/utils/tagUtil';
import { formatToDate } from '/@/utils/dateUtil';
import { GetPositionModel } from '../../../api/position/model/positionModel';
import { Rule } from '/@/components/Form';

export const getPositionOptions = async (id: Nullable<Number>, isAll: boolean) => {
  let positionList: GetPositionModel[] = [];
  if (id) {
    positionList = await getOrganizationPositionList(id, isAll);
  } else {
    positionList = await getPositionList({});
  }
  const positionOptions = positionList.reduce((prev, next: Recordable) => {
    if (next) {
      prev.push({
        ...omit(next, ['name', 'id']),
        label: next['name'],
        value: next['id'],
        disabled:
          next['status'] === Status.Invalid ||
          (next['isBelong'] == false && next['isPublic'] === false),
      });
    }
    return prev;
  }, [] as OptionsItem[]);
  return positionOptions;
};

export const columns: BasicColumn[] = [
  {
    title: '名称',
    dataIndex: 'name',
    width: 120,
    slots: { customRender: 'name' },
  },
  {
    title: '状态',
    dataIndex: 'status',
    width: 80,
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
    width: 80,
  },
  {
    title: '备注',
    dataIndex: 'remark',
    width: 120,
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

export const positionSchemas: FormSchema[] = [
  {
    field: 'name',
    component: 'Input',
    label: '名称',
    colProps: {
      span: 12,
    },
  },
  {
    field: 'sort',
    component: 'InputNumber',
    componentProps: {
      style: 'width:100%',
    },
    label: '排序',
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
    colProps: {
      span: 24,
    },
  },
];

export const positionDetailSchemas: DescItem[] = [
  {
    field: 'name',
    label: '名称',
  },
  {
    field: 'sort',
    label: '排序',
  },
  {
    field: 'remark',
    label: '备注',
    span: 2,
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

const checkPositionRule = async (value: string, id: Nullable<number>) => {
  if (value) {
    const exist = await checkPosition({
      id: id,
      name: value,
    });
    if (exist) {
      return Promise.reject(`已经存在${value}的职位`);
    }
  }
  return Promise.resolve();
};

export const getNameRules = (id: Nullable<number>): Rule[] => {
  return [
    {
      required: true,
      message: '岗位名称不允许为空',
    },
    {
      max: 50,
      message: '岗位名称长度不允许超过50个字符',
      validateTrigger: ['change', 'blur'],
    },
    {
      type: 'string',
      validateTrigger: ['change', 'blur'],
      validator: (rules, value) => {
        return checkPositionRule(value, id);
      },
    },
  ];
};
