<template>
  <BasicDrawer v-bind="$attrs" @register="registerDrawer" :title="getTitle" width="50%">
    <Card title="基础信息" :bordered="false">
      <Description
        size="middle"
        :column="2"
        :data="roleDetail"
        :schema="roleDetailSchemas"
        class="enter-y"
      />
    </Card>
    <Card title="授权信息" :bordered="false">
      <Row :gutter="24">
        <Col :span="12">
          <div>菜单权限</div>
          <BasicTree :tree-data="menusTreeData" checkable disabled ref="menuTreeRef" />
        </Col>
        <Col :span="12">
          <div>数据权限: {{ dataRangeText }}</div>
          <List v-if="showOrganizationTree" :data-source="customOrganizationList">
            <template #renderItem="{ item }">
              <ListItem>{{ item.title }}</ListItem>
            </template>
          </List>
        </Col>
      </Row>
    </Card>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, unref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { BasicTree, TreeActionType } from '/@/components/Tree/index';
  import { Card, Row, Col, List, ListItem } from 'ant-design-vue';
  import { Description } from '/@/components/Description/index';
  import { TreeItem } from '/@/components/Tree';
  import { roleDetailSchemas } from './role.data';
  import { DataRange } from '/@/utils/dataRangeUtil';
  import { getMenuTreeList2 } from '/@/views/authorization/menu/menu.data';
  import { getOrganizationTreeList } from '/@/views/authorization/organization/organization.data';
  import { treeToList } from '/@/utils/helper/treeHelper';

  export default defineComponent({
    name: 'RoleDetailDrawer',
    components: { BasicDrawer, Card, Description, Row, Col, BasicTree, List, ListItem },
    setup() {
      const getTitle = ref('角色');
      const roleDetail = ref();
      const menuTreeRef = ref<Nullable<TreeActionType>>(null);
      const menusTreeData = ref<TreeItem[]>([]);

      const organizationTreeRef = ref<Nullable<TreeActionType>>(null);
      const organizationTreeData = ref<TreeItem[]>([]);

      const showOrganizationTree = ref<boolean>(false);
      const dataRangeText = ref<string>();
      const customOrganizationList = ref([]);

      function getMenuTree() {
        const tree = unref(menuTreeRef);
        if (!tree) {
          throw new Error('tree is null!');
        }
        return tree;
      }

      function setDataRange(dataRange: DataRange) {
        showOrganizationTree.value = dataRange === DataRange.CustomOrganization;
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

      function getOrganizationTree() {
        const tree = unref(organizationTreeRef);
        if (!tree) {
          throw new Error('tree is null!');
        }
        return tree;
      }

      function setMenusTreeData(treeData: TreeItem[]) {
        menusTreeData.value = treeData;
      }

      function setOrganizationTreeData(treeData: TreeItem[]) {
        menusTreeData.value = treeData;
      }
      const [registerDrawer] = useDrawerInner(async (data) => {
        getTitle.value = data.realName;
        roleDetail.value = data;
        const menuTree = await getMenuTreeList2({});
        setMenusTreeData(menuTree);
        getMenuTree().setCheckedKeys(data.menuIds);
      //  getMenuTree().expandAll(true);
        setDataRange(data.dataRange);
        const allOrganizationList = treeToList(await getOrganizationTreeList());
        customOrganizationList.value = allOrganizationList.filter(
          (item) => data.customOrganizationIds.indexOf(item.key) >= 0,
        );
        debugger;
      });
      return {
        getTitle,
        roleDetail,
        roleDetailSchemas,
        menuTreeRef,
        menusTreeData,
        organizationTreeRef,
        organizationTreeData,
        showOrganizationTree,
        dataRangeText,
        customOrganizationList,
        getOrganizationTree,
        setOrganizationTreeData,
        getMenuTree,
        setMenusTreeData,
        setDataRange,
        registerDrawer,
      };
    },
  });
</script>
