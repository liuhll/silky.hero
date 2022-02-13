<template>
  <BasicDrawer v-bind="$attrs" @register="registerDrawer" :title="getTitle" width="50%">
    <Tabs>
      <TabPane key="1" tab="详情">
        <Card title="基础信息" :bordered="false">
          <Description
            size="middle"
            :column="2"
            :data="userDetail"
            :schema="userDetailSchemas"
            class="enter-y"
          />
        </Card>
        <Card title="组织信息" :bordered="false">
          <UserSubsidiaryTable ref="userSubsidiaryTableRef" :editable="false" />
        </Card>
      </TabPane>
      <TabPane key="2" tab="登陆日志">
        <Empty v-if="userLoginLog.length <= 0" />
      </TabPane>
    </Tabs>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, unref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { Card, Empty, Tabs, TabPane, Collapse, CollapsePanel } from 'ant-design-vue';
  import { Description } from '/@/components/Description/index';
  import UserSubsidiaryTable from './UserSubsidiaryTable.vue';
  import { userDetailSchemas } from './user.data';
  export default defineComponent({
    name: 'UserDetailDrawer',
    components: { BasicDrawer, UserSubsidiaryTable, Card, Tabs, TabPane, Description, Empty },
    setup() {
      const userSubsidiaryTableRef = ref<{
        getDataSource: () => any;
        setTableData: (data: any[]) => void;
        setOrganizaionTreeList: () => void;
        setPositionOptions: () => void;
      } | null>(null);
      const getTitle = ref('用户');
      const userDetail = ref();
      const userLoginLog = ref([]);
      const [registerDrawer] = useDrawerInner(async (data) => {
        getTitle.value = data.realName;
        userDetail.value = data;
        await userSubsidiaryTableRef.value?.setOrganizaionTreeList();
        await userSubsidiaryTableRef.value?.setPositionOptions();
        userSubsidiaryTableRef.value?.setTableData(data.userSubsidiaries);
      });
      return {
        getTitle,
        userSubsidiaryTableRef,
        userDetail,
        userDetailSchemas,
        userLoginLog,
        registerDrawer,
      };
    },
  });
</script>
