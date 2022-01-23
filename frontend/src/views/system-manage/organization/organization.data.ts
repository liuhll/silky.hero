import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { statusOptions } from '/@/utils/status';
import { Tag } from 'ant-design-vue';
import { Icon } from '/@/components/Icon';

export const userColumns: BasicColumn[] = [
  {
    title: '用户名',
    dataIndex: 'userName',
    width: 120,
  },
  {
    title: '真实姓名',
    dataIndex: 'realName',
    width: 120,
  },
  {
    title: '邮箱',
    dataIndex: 'email',
    width: 120,
  },
  {
    title: '手机',
    dataIndex: 'telPhone',
    width: 120,
  },
  {
    title: '工号',
    dataIndex: 'jobNumber',
    width: 120,
  },
]

export const organizationFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: '名称',
    component: 'Input',
    helpMessage: ['请输入组织机构名称'],
    rules: [
      {
        required: true,
        message: '请输入用户名',
      },
    ],
  },
  {
    field: 'sort',
    label: '排序',
    component: 'InputNumber',
    helpMessage: ['请输入排序'],
  },
  {
    field: 'status',
    label: '状态',
    component: 'RadioButtonGroup',
    defaultValue: 1,
    componentProps: {
      options: statusOptions,
    },
    helpMessage: ['请选择状态'],
  },
  {
    field: 'remark',
    label: '备注',
    component: 'Input',
    helpMessage: ['请输入备注'],
  }
]
