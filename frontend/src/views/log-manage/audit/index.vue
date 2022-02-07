<template>
  <PageWrapper>
    <BasicTable @register="registerTable" :searchInfo="searchInfo">
      <template #url="{ text, record }">
        <Tag :color="setHttpResponseStatusColor(record.httpStatusCode)" v-if="record.httpStatusCode"
          >{{ record.httpStatusCode }}
        </Tag>
        &nbsp;
        <Tag :color="setHttpMethodColor(record.httpMethod)" v-if="record.httpMethod">{{
          record.httpMethod
        }}</Tag>
        &nbsp; {{ text }}
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:info-standard-line',
              tooltip: '查看日志详情',
              onClick: handleView.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
    <AuditLogDrawer @register="registerDrawer" />
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, nextTick } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { getAuditLogPageList, getAuditLogDetail } from '/@/api/auditlog';
  import { columns, searchFormSchema } from './auditlog.data';
  import { PageWrapper } from '/@/components/Page';
  import AuditLogDrawer from './AuditLogDrawer.vue';
  import { useDrawer } from '/@/components/Drawer';
  import { Tag } from 'ant-design-vue';

  export default defineComponent({
    name: 'AuditLog',
    components: { PageWrapper, BasicTable, TableAction, AuditLogDrawer, Tag },
    setup() {
      const searchInfo = ref({});
      const [registerTable] = useTable({
        title: '审计日志列表',
        rowKey: 'id',
        columns,
        api: getAuditLogPageList,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
        },
        beforeFetch: (formSearch) => {
          if (formSearch.startTime) {
            formSearch.startTime = formSearch.startTime.format('YYYY-MM-DD HH:mm:ss');
          }
          if (formSearch.endTime) {
            formSearch.endTime = formSearch.endTime.format('YYYY-MM-DD HH:mm:ss');
          }
          return formSearch;
        },
        useSearchForm: true,
        showTableSetting: true,
        bordered: true,
        actionColumn: {
          width: 120,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        },
      });
      const [registerDrawer, { openDrawer }] = useDrawer();
      function handleView(record: Recordable) {
        nextTick(async () => {
          const auditLogDetail = await getAuditLogDetail(record.id);
          openDrawer(true, { ...auditLogDetail });
        });
      }

      function setHttpResponseStatusColor(httpResponseStatus: number): string {
        let color = 'green';
        switch (httpResponseStatus) {
          case 200:
            color = 'green';
            break;
          case 301:
          case 302:
            color = 'yellow';
            break;
          default:
            color = 'red';
            break;
        }
        return color;
      }
      function setHttpMethodColor(httpMethod: stirng): string {
        let color = 'green';
        switch (httpMethod.toUpperCase()) {
          case 'GET':
            color = 'green';
            break;
          case 'POST':
            color = 'blue';
            break;
          case 'PUT':
          case 'PATCH':
            color = 'yellow';
            break;
          case 'DELETE':
            color = 'red';
            break;
        }
        return color;
      }
      return {
        registerTable,
        registerDrawer,
        handleView,
        setHttpResponseStatusColor,
        setHttpMethodColor,
        searchInfo,
      };
    },
  });
</script>
