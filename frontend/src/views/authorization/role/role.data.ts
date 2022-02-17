import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { statusOptions } from '/@/utils/status';
import { Status } from '/@/utils/status';
import { getRoleList } from '/@/api/role';
import { omit } from 'lodash-es';
import { OptionsItem } from '/@/utils/model';
import { Tag } from 'ant-design-vue';
import { h } from 'vue';
import { formatToDate } from '/@/utils/dateUtil';
import { DataRange } from '/@/utils/dataRangeUtil';
import { DescItem } from '/@/components/Description/index';
import { commonTagRender } from '/@/utils/tagUtil';

export const columns: BasicColumn[] = [
  {
    title: '标识',
    dataIndex: 'name',
    width: 120,
  },
  {
    title: '名称',
    dataIndex: 'realName',
    width: 120,
    slots: { customRender: 'realName' },
  },
  {
    title: '状态',
    dataIndex: 'status',
    width: 80,
    customRender: ({ record }) => {
      const enable = record.status === Status.Valid;
      const color = enable ? 'green' : 'red';
      const text = enable ? '启用' : '停用';
      return h(Tag, { color: color }, () => text);
    },
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

export const roleSchemas: FormSchema[] = [
  {
    field: 'name',
    component: 'Input',
    label: '角色标识',
    helpMessage: '英文字符和数字组合,不允许超过50个字符',
    rules: [
      {
        required: true,
        message: '角色不允许为空',
      },
      {
        max: 50,
        message: '角色标识长度不允许超过50个字符',
        validateTrigger: ['change', 'blur'],
      },
      {
        type: 'string',
        pattern: new RegExp('^\\w+$'),
        message: '角色格式不正确',
        validateTrigger: ['change', 'blur'],
      },
    ],
  },
  {
    field: 'realName',
    component: 'Input',
    label: '角色名称',
    rules: [
      {
        required: true,
        message: '角色名称不允许为空',
      },
      {
        max: 50,
        message: '角色名称长度不允许超过50个字符',
        validateTrigger: ['change', 'blur'],
      },
    ],
  },
  {
    field: 'sort',
    component: 'InputNumber',
    componentProps: {
      style: { width: '100%' },
    },
    label: '排序',
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
  },
  {
    field: 'isPublic',
    label: '公开',
    component: 'RadioButtonGroup',
    componentProps: {
      options: [
        { label: '否', value: false },
        { label: '是', value: true },
      ],
    },
    defaultValue: false,
  },
  {
    field: 'status',
    label: '状态',
    component: 'RadioButtonGroup',
    componentProps: {
      options: statusOptions,
    },
    defaultValue: Status.Valid,
  },
  {
    field: 'remark',
    component: 'InputTextArea',
    label: '备注',
  },
];

export const roleDataSchemas: FormSchema[] = [
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
        message: '请选择角色有的数据权限部门',
      },
    ],
    ifShow: ({ values }) => {
      return values.dataRange === DataRange.CustomOrganization.valueOf();
    },
  },
];

export const getRoleOptions = async (query:any) => {
  const roleList = await getRoleList(query);
  const positionOptions = roleList.reduce((prev, next: Recordable) => {
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
  return positionOptions;
};

export const roleDetailSchemas: DescItem[] = [
  {
    label: '标识',
    field: 'name',
  },
  {
    label: '角色名',
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
    label: '状态',
    field: 'status',
    render: (value) => {
      if (value === Status.Valid) {
        return commonTagRender('blue', '正常');
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
