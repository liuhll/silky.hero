<template>
  <PageWrapper v-loading="loadingRef" loading-tip="加载中...">
    <BasicTable @register="registerTable" :searchInfo="searchInfo">
      <template #toolbar>
        <a-button type="primary" v-auth="'Identity.Dictionary.Create'" @click="handleCreate"
          >新增类型</a-button
        >
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:info-standard-line',
              tooltip: '查看字典详情',
              onClick: handleView.bind(null, record),
              auth: 'Identity.Dictionary.LookDetail',
            },
            {
              icon: 'clarity:note-edit-line',
              tooltip: '编辑字典资料',
              onClick: handleEdit.bind(null, record),
              auth: 'Identity.Dictionary.Update',
            },
            {
              icon: 'ant-design:delete-outlined',
              color: 'error',
              tooltip: '删除此字典',
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
              },
              auth: 'Identity.Dictionary.Delete',
            },
          ]"
        />
      </template>
    </BasicTable>
    <DictionaryDrawer @register="registerDrawer" @success="handleSuccess" />
    <DictionaryMenuDrawer
      @register="registerDictionaryMenuDrawer"
      @success="handleSuccessAuthorizeDictionaryMenu"
      ref="dictionaryMenuDrawerRef"
    />
    <DictionaryDataDrawer
      @register="registerDictionaryDataDrawer"
      @success="handleSuccessAuthorizeDictionaryData"
      ref="dictionaryDataDrawerRef"
    />
    <DictionaryDetailDrawer @register="registerDictionaryDetailDrawer" />
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, unref, computed, nextTick } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import {
    getDictionaryPageList,
    createDictionary,
    updateDictionary,
    deleteDictionary,
  } from '/@/api/dictionary';
  import { columns, searchFormSchema } from './dictionary.data';
  import { PageWrapper } from '/@/components/Page';
  import DictionaryDrawer from './DictionaryDrawer.vue';
  import DictionaryDetailDrawer from './DictionaryDetailDrawer.vue';
  import { useDrawer } from '/@/components/Drawer';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { usePermission } from '/@/hooks/web/usePermission';
  export default defineComponent({
    name: 'Dictionary',
    components: {
      PageWrapper,
      BasicTable,
      TableAction,
      DictionaryDrawer,
      DictionaryMenuDrawer,
      DictionaryDataDrawer,
      DictionaryDetailDrawer,
    },
    setup() {
      const searchInfo = ref({});
      const { notification } = useMessage();
      const { hasPermission } = usePermission();
      const showSearchForm = computed(() => hasPermission('Identity.Dictionary.Search'));
      const loadingRef = ref(false);
      const tableConfig: any = {
        title: '字典列表',
        rowKey: 'id',
        columns,
        api: getDictionaryPageList,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
        },
        useSearchForm: unref(showSearchForm),
        showTableSetting: true,
        bordered: true,
      };
      if (
        hasPermission([
          'BasicData.Dictionary.LookDetail',
          'BasicData.Dictionary.Update',
          'BasicData.Dictionary.Delete',
        ])
      ) {
        tableConfig.actionColumn = {
          width: 120,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        };
      }
      const [registerTable, { reload }] = useTable(tableConfig);

      const [registerDrawer, { openDrawer }] = useDrawer();
      const [registerDictionaryDetailDrawer, { openDrawer: openDictionaryDetailDrawer }] = useDrawer();
      function handleCreate() {
        openDrawer(true, {
          isUpdate: false,
        });
      }
      function handleSuccess(data) {
        nextTick(async () => {
          try {
            loadingRef.value = true;
            const isUpdate = !!data?.isUpdate;
            if (isUpdate) {
              await updateDictionary(data.values);
              loadingRef.value = false;
              // updateTableDataRecord(data.values.id, data.values);
              notification.success({
                message: `更新字典${data.values.name}成功.`,
              });
            } else {
              await createDictionary(data.values);
              loadingRef.value = false;
              notification.success({
                message: `新增字典${data.values.name}成功.`,
              });
            }
            reload();
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      function handleView(record: Recordable) {
        nextTick(() => {
          openDictionaryDetailDrawer(true, record.id);
        });
      }

      function handleEdit(record: Recordable) {
        openDrawer(true, {
          isUpdate: true,
          record,
        });
      }
      function handleDelete(record: Recordable) {
        nextTick(async () => {
          try {
            loadingRef.value = true;
            await deleteDictionary(record.id);
            loadingRef.value = false;
            notification.success({
              message: `删除字典${record.name}成功.`,
            });
            reload();
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }
      return {
        registerTable,
        handleCreate,
        registerDrawer,
        registerDictionaryDetailDrawer,
        handleSuccess,
        handleView,
        handleEdit,
        handleDelete,
        searchInfo,
        loadingRef,
      };
    },
  });
</script>
