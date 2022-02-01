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
