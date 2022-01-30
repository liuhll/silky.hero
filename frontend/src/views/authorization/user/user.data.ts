import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';

export const columns: BasicColumn[] = [
  {
    title: '用户名',
    dataIndex: 'userName',
    width: 100,
  },
  {
    title: '真实姓名',
    dataIndex: 'realName',
    width: 80,
  },
  {
    title: '昵称',
    dataIndex: 'surname',
    width: 80,
  },
  {
    title: '邮箱',
    dataIndex: 'email',
    width: 100,
  },
  {
    title: '生日',
    dataIndex: 'birthDay',
    width: 80,
  },
  {
    title: '性别',
    dataIndex: 'sex',
    width: 50,
  },
  {
    title: '手机',
    dataIndex: 'mobilePhone',
    width: 80,
  },
  {
    title: '工号',
    dataIndex: 'jobNumber',
    width: 80,
  },
  {
    title: '机构(职位)',
    dataIndex: 'userSubsidiaries',
    width: 100,
    slots: { customRender: 'userSubsidiaries' },
  },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'userName',
    label: '用户名',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'realName',
    label: '真实姓名',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'mobilePhone',
    label: '手机',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'email',
    label: '电子邮件',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'organizationIds',
    label: '组织机构',
    component: 'TreeSelect',
    slot: 'organizationTree',
    colProps: { span: 6 },
  },
  {
    field: 'positionIds',
    label: '职位',
    component: 'Select',
    slot: 'positionIdSelect',
    colProps: { span: 6 },
  },
];
