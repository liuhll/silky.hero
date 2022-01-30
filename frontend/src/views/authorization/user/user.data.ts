import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { getOrganizationTree } from '/@/api/organization';

export const columns: BasicColumn[] = [
  {
    title: '用户名',
    dataIndex: 'userName',
    width: 120,
  },
  {
    title: '真实姓名',
    dataIndex: 'realName',
    width: 100,
  },
  {
    title: '昵称',
    dataIndex: 'surname',
    width: 100,
  },
  {
    title: '邮箱',
    dataIndex: 'email',
    width: 100,
  },
  {
    title: '生日',
    dataIndex: 'birthDay',
    width: 100,
  },
  {
    title: '性别',
    dataIndex: 'sex',
    width: 50,
  },
  {
    title: '手机',
    dataIndex: 'mobilePhone',
    width: 100,
  },
  {
    title: '工号',
    dataIndex: 'jobNumber',
    width: 100,
  },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'userName',
    label: '用户名',
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'realName',
    label: '真实姓名',
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'mobilePhone',
    label: '手机',
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'email',
    label: '电子邮件',
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'organizationIds',
    label: '组织机构',
    component: 'TreeSelect',
    slot: 'organizationTree',
    colProps: { span: 8 },
  },
];
