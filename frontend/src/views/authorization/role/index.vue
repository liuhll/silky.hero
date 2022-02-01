<template>
  <PageWrapper>
    <BasicTable @register="registerTable" :searchInfo="searchInfo">
      <template #realName="{ text, record }">
        {{ text }}
        <Tag color="green" v-if="record.isDefault">默认</Tag>
        <Tag color="blue" v-if="record.isPublic">公开</Tag>
      </template>
    </BasicTable>
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, onMounted, unref, nextTick } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { getRolePageList } from '/@/api/role';
  import { columns, searchFormSchema } from './role.data';
  import { PageWrapper } from '/@/components/Page';
  export default defineComponent({
    name: 'Role',
    components: { PageWrapper, BasicTable, Tag },
    setup() {
      const searchInfo = ref({});
      const [registerTable, { reload }] = useTable({
        title: '角色列表',
        rowKey: 'id',
        columns,
        api: getRolePageList,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
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
      return {
        registerTable,
        searchInfo,
      };
    },
  });
</script>
