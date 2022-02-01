import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';

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
    rules: [
      {
        required: true,
        message: '角色标示不允许为空',
      },
      {
        max: 50,
        message: '角色标示长度不允许超过50个字符',
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
    field: 'isDefault',
    label: '默认',
    component: 'RadioButtonGroup',
    componentProps: {
      options: [
        { label: '是', value: true },
        { label: '否', value: false },
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
        { label: '是', value: true },
        { label: '否', value: false },
      ],
    },
    defaultValue: false,
  },
];
