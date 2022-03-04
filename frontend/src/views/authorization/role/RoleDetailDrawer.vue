<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    :title="getTitle"
    width="50%"
    destroyOnClose
  >
    <Tabs>
      <TabPane key="1" tab="基础信息">
        <Description
          size="middle"
          :column="2"
          :data="roleDetail"
          :schema="roleDetailSchemas"
          class="enter-y"
        />
      </TabPane>
      <TabPane key="2" tab="菜单权限">
        <Empty v-if="menusTreeData.length <= 0" />
        <AuthorizationMenu
          v-if="menusTreeData.length > 0"
          :checkable="false"
          :tree-items="menusTreeData"
        />
      </TabPane>
      <TabPane key="3" tab="数据权限">
        <div style="margin-left: 20px; margin-top: 5px">数据权限: {{ dataRangeText }}</div>
        <List
          v-if="showCustomOrganizationDataRanges"
          :data-source="roleDetail.customOrganizationDataRanges"
          bordered
          style="margin-left: 20px; margin-top: 15px"
        >
          <template #renderItem="{ item }">
            <ListItem>{{ item.name }}</ListItem>
          </template>
        </List>
      </TabPane>
    </Tabs>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { Tabs, TabPane, List, ListItem, Empty } from 'ant-design-vue';
  import { Description } from '/@/components/Description/index';
  import { roleDetailSchemas } from './role.data';
  import { DataRange } from '/@/utils/dataRangeUtil';
  import { treeMap } from '/@/utils/helper/treeHelper';
  import { getRoleDetailById } from '/@/api/role';
  import { AuthorizationMenu } from '../components/AuthorizationMenu/index';
  import { TreeItem } from '/@/components/Tree';

  export default defineComponent({
    name: 'RoleDetailDrawer',
    components: {
      BasicDrawer,
      Description,
      Tabs,
      TabPane,
      AuthorizationMenu,
      List,
      ListItem,
      Empty,
    },
    setup() {
      const getTitle = ref('角色');
      const roleDetail = ref();
      const dataRangeText = ref<string>();
      const showCustomOrganizationDataRanges = ref<boolean>(false);
      const menusTreeData = ref<TreeItem[]>([]);

      function setDataRange(dataRange: DataRange) {
        showCustomOrganizationDataRanges.value = dataRange === DataRange.CustomOrganization;
        let text = '';
        switch (dataRange) {
          case DataRange.Whole:
            text = '所有部门';
            break;
          case DataRange.SelfOrganization:
            text = '本部门';
            break;
          case DataRange.SelfAndChildrenOrganization:
            text = '本部门及子部门';
            break;
          case DataRange.CustomOrganization:
            text = '自定义部门';
            break;
        }
        dataRangeText.value = text;
      }
      const [registerDrawer] = useDrawerInner(async (id) => {
        const data = await getRoleDetailById(id);
        getTitle.value = data.realName;
        roleDetail.value = data;
        menusTreeData.value = treeMap(data.menus, {
          conversion: (node: any) => {
            return {
              title: node.title,
              key: node.menuId,
              value: node.menuId,
            };
          },
        });
        setDataRange(data.dataRange);
      });
      return {
        registerDrawer,
        getTitle,
        roleDetail,
        roleDetailSchemas,
        menusTreeData,
        showCustomOrganizationDataRanges,
        dataRangeText,
      };
    },
  });
</script>

<style>
  .ant-tabs-content {
    height: 100%;
    min-height: 200px;
  }
</style>
