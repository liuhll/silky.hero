/* eslint-disable prettier/prettier */
import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { formatToDate } from '/@/utils/dateUtil';
import { DescItem } from '/@/components/Description/index';
import { h } from 'vue';
import { Tag } from 'ant-design-vue';

const commonTagRender = (color: string, curVal: string) => h(Tag, { color }, () => curVal);

export const columns: BasicColumn[] = [
  {
    title: 'http请求',
    dataIndex: 'url',
    width: 200,
    align: 'left',
    slots: { customRender: 'url' },
  },
  {
    title: '用户',
    dataIndex: 'userName',
    width: 120,
  },
  {
    title: 'IP地址',
    dataIndex: 'clientIpAddress',
    width: 120,
  },
  {
    title: '时间',
    dataIndex: 'executionTime',
    width: 120,
    format: (value) => {
      return formatToDate(value, 'YYYY-MM-DD HH:mm:ss');
    },
  },
  {
    title: '持续时间',
    dataIndex: 'executionDuration',
    width: 120,
  },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'startTime',
    label: '开始时间',
    component: 'DatePicker',
    colProps: { span: 6 },
    componentProps: {
      showTime: true,
      format: 'YYYY-MM-DD HH:mm:ss',
      style: 'width:100%',
    },
  },
  {
    field: 'endTime',
    label: '结束时间',
    component: 'DatePicker',
    colProps: { span: 6 },
    componentProps: {
      showTime: true,
      format: 'YYYY-MM-DD HH:mm:ss',
      style: 'width:100%',
    },
  },
  {
    field: 'url',
    label: 'url过滤',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'httpMethod',
    label: 'Http方法',
    component: 'Select',
    componentProps: {
      options: [
        {
          lable: 'GET',
          value: 'GET',
        },
        {
          lable: 'POST',
          value: 'POST',
        },
        {
          label: 'PUT',
          value: 'PUT',
        },
        {
          label: 'PATCH',
          value: 'PATCH',
        },
        {
          label: 'DELETE',
          value: 'DELETE',
        },
      ],
    },
    colProps: { span: 6 },
  },
  {
    field: 'minExecutionDuration',
    label: '最小持续时间',
    component: 'InputNumber',
    componentProps: {
      style: 'width:100%'
    },
    colProps: { span: 6 },
  },
  {
    field: 'maxExecutionDuration',
    label: '最大持续时间',
    component: 'InputNumber',
    componentProps: {
        style: 'width:100%'
    },
    colProps: { span: 6 },
  },
  {
    field: 'userName',
    label: '用户名',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'httpStatusCode',
    label: 'Http状态码',
    component: 'Select',
    componentProps: {
      options: [
        {
            label: '200-成功',
            value: 200,
        },
        // {
        //     label: '201-Created',
        //     value: 201,
        // },
        // {
        //     label: '202-Accepted',
        //     value: 202,
        // },
        {
            label: '301-重定向',
            value: 301,
        },
        {
            label: '400-错误请求',
            value: 400,
        },
        {
            label: '401-未认证',
            value: 401,
        },
        {
            label: '403-未授权',
            value: 403,
        },
        {
            label: '404-不存在路由',
            value: 404,
        },
        {
            label: '500-服务器异常',
            value: 500,
        },
        {
            label: '501-未实现接口',
            value: 501,
        },
        {
            label: '502-网关错误',
            value: 502,
        },
        {
            label: '503-服务不可达',
            value: 503,
        },
        {
            label: '504-网关超时',
            value: 504,
        },
        {
            label: '505-不支持的HTTP方法',
            value: 504,
        },
      ],
    },
    colProps: { span: 6 },
  },
  {
    field: 'hasException',
    label: '存在异常',
    component: 'Select',
    componentProps: {
      options: [{
        label: '是',
        value: true,
      }, {
        label: '否',
        value: false,
      },],
    },
    colProps: { span: 6 },
  },
];


export const auditLogDetailSchemas: DescItem[] = [
  {
    field: 'httpStatusCode',
    label: 'http状态码',
    labelMinWidth: 70,
    render: (value) => commonTagRender('blue',value),
  },
  {
    field: 'httpMethod',
    label: 'http方法',
    labelMinWidth: 70,
    render: (value) => commonTagRender('green',value),
  },
  {
    field: 'url',
    label: 'url',
    contentMinWidth: 160,
  },
  {
    field: 'requestParameters',
    label: '请求参数',
  },
  {
    field: 'clientIpAddress',
    label: '客户端Ip',
  },
  {
    field: 'exceptionMessage',
    label: '异常',
  },
  {
    field: 'userName',
    label: '用户名',
  },
  {
    field: 'executionTime',
    label: '时间',
    render: (value) => formatToDate(value, 'YYYY-MM-DD HH:mm:ss'),
  },
  {
    field: 'executionDuration',
    label: '持续时间',
  },
  {
    field: 'browserInfo',
    label: '浏览器信息',
  },
  {
    field: 'clientId',
    label: '关联Id',
  },
]

export const auditLogActionSchemas: DescItem[] = [
  {
    field: 'hostName',
    label: '所属应用',
    labelMinWidth: 70,
    contentMinWidth: 120,
  },
  {
    field: 'hostAddress',
    label: '主机实例地址',
    labelMinWidth: 80,
  },
  {
    field: 'serviceName',
    label: '服务名称',
  },
  {
    field: 'serviceEntryId',
    label: '服务条目',
  },
  {
    field: 'serviceKey',
    label: 'serviceKey',
  },
  {
    field: 'parameters',
    label: '参数',
  },
    {
    field: 'executionTime',
    label: '执行时间',
    render: (value) => formatToDate(value, 'YYYY-MM-DD HH:mm:ss'),
  },
      {
    field: 'executionDuration',
    label: '执行时长',
  },
  {
    field: 'exceptionMessage',
    label: '异常',
  },
  {
    field: 'isDistributedTransaction',
    label: '是否是分布式事务',
  },
]
