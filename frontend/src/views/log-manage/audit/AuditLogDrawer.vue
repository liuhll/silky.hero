<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    title="审计日志详情"
    destroyOnClose
    width="50%"
  >
    <Tabs default-active-key="1">
      <TabPane key="1" tab="总体">
        <Description
          size="middle"
          :column="2"
          :data="auditLogDetail"
          :schema="auditLogDetailSchemas"
          class="enter-y"
        />
      </TabPane>
      <TabPane key="2" :tab="getOperateTitile">
        <Empty v-if="auditLogDetail.actions.length <= 0" />
        <Collapse v-if="auditLogDetail.actions.length >= 0" accordion>
          <CollapsePanel
            v-for="(item, index) in auditLogDetail.actions"
            :key="index"
            :header="item.methodName"
          >
            <Description
              size="middle"
              :column="2"
              :data="item"
              :schema="auditLogActionSchemas"
              
              class="my-4 enter-y"
            />
          </CollapsePanel>
        </Collapse>
      </TabPane>
    </Tabs>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, unref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { GetAuditLogDetailModel } from '/@/api/auditlog/model/auditLogModel';
  import { auditLogDetailSchemas, auditLogActionSchemas } from './auditlog.data';
  import { Description } from '/@/components/Description/index';
  import { Empty, Tabs, TabPane, Collapse, CollapsePanel } from 'ant-design-vue';
  export default defineComponent({
    name: 'AuditLogDrawer',
    components: { BasicDrawer, Description, Tabs, TabPane, Collapse, CollapsePanel, Empty },
    setup() {
      const auditLogDetail = ref<GetAuditLogDetailModel>({});
      const getOperateTitile = ref<string>(`操作(0)`);
      // computed(() => {
      //   if (unref(auditLogDetail).actions?.length > 0) {
      //     return `操作(${unref(auditLogDetail).actions?.length})`;
      //   }
      //   return `操作(0)`;
      // });
      const [registerDrawer, { setDrawerProps }] = useDrawerInner(async (data) => {
        auditLogDetail.value = data;
        if (unref(auditLogDetail).actions?.length > 0) {
          getOperateTitile.value = `操作(${unref(auditLogDetail).actions?.length})`;
        } else {
          getOperateTitile.value = `操作(0)`;
        }
        setDrawerProps({ confirmLoading: false });
      });
      return {
        registerDrawer,
        auditLogDetail,
        auditLogDetailSchemas,
        auditLogActionSchemas,
        getOperateTitile,
      };
    },
  });
</script>
