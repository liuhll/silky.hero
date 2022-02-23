<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="40%"
    @ok="handleSubmit"
  >
    <Checkbox
      v-model:checked="checkAll"
      :indeterminate="indeterminate"
      @change="handldCheckAllMenus"
      style="margin-left: 30px; margin-bottom: 20px"
      >全选</Checkbox
    >
    <Tabs tab-position="left">
      <TabPane
        v-for="(roleItem, index) in roleItems"
        :key="`${index}`"
        :tab="`${roleItem.group}(${roleItem.checkNum})`"
      >
        <Checkbox
          v-model:checked="roleItem.checkAll"
          :indeterminate="roleItem.indeterminate"
          style="margin-left: 20px; margin-bottom: 10px"
          @change="handldCheckGroupMenus(roleItem)"
          >全选</Checkbox
        >
        <BasicTree
          :tree-data="roleItem.treeData"
          :checkedKeys="roleItem.checkedMenuIds"
          :ref="`treeRef${index}`"
          @check="(k, e) => handleCheckMenus(k, e, roleItem)"
          defaultExpandAll
          :clickRowToExpand="false"
          checkable
          :selectable="false"
          multiple
        />
      </TabPane>
    </Tabs>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref, reactive, toRefs } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { BasicTree } from '/@/components/Tree/index';
  import { TreeItem } from '/@/components/Tree';
  import { Checkbox, Tabs, TabPane } from 'ant-design-vue';
  import { treeToList } from '/@/utils/helper/treeHelper';
  import { getMenuTreeList2 } from '/@/views/authorization/menu/menu.data';
  import { getRoleMenuIds } from '/@/api/role';
  import { RoleItem } from './role.data';
  import { DataNode } from 'ant-design-vue/lib/tree';
  export default defineComponent({
    name: 'RoleMenuDrawer',
    components: { BasicDrawer, BasicTree, Checkbox, Tabs, TabPane },
    setup(_, { emit }) {
      const getTitle = computed(() => '授权角色菜单');
      // const menusTreeData = ref<TreeItem[]>([]);
      const roleId = ref<Nullable<number>>();
      const roleItems = ref<RoleItem[]>([]);
      const checkAllState = reactive({
        indeterminate: false,
        checkAll: false,
      });

  
      function setCheckAllStateStatus() {
        const checkAll =
          unref(roleItems).filter((item) => item.checkAll === true).length ===
          unref(roleItems).length;
        const indeterminate =
          !checkAll && unref(roleItems).filter((item) => item.checkAll === true).length > 0;
        checkAllState.indeterminate = indeterminate;
        checkAllState.checkAll = checkAll;
      }

      function handldCheckAllMenus(e: any) {
        debugger
        if (e.target.isChecked) {
          for(const roleItem in roleItems.value) {

          }
        }
      }

      function getRoleCheckedMenuInfo(dataNode: DataNode[], menuIds: Number[]) {
        const roleCheckedMenuInfo: any = {};
        const dataNodeList = treeToList(dataNode);
        roleCheckedMenuInfo.menuIds = dataNodeList
          .filter((item) => menuIds.indexOf(item.key) > 0)
          .map((item) => item.key);
        roleCheckedMenuInfo.checkAll = roleCheckedMenuInfo.menuIds.length === dataNodeList.length;
        roleCheckedMenuInfo.indeterminate =
          roleCheckedMenuInfo.menuIds.length > 0 && roleCheckedMenuInfo.checkAll !== true;
        roleCheckedMenuInfo.checkNum = roleCheckedMenuInfo.menuIds.length;
        return roleCheckedMenuInfo;
      }

      function createRoleItem(menuTree: TreeItem, menuIds: Number[]) {
        let roleItem: RoleItem = {
          group: menuTree.title,
          treeData: menuTree.children,
        };
        const roleCheckedMenuInfo: any = getRoleCheckedMenuInfo(menuTree.children, menuIds);
        roleItem.checkNum = roleCheckedMenuInfo.checkNum;
        roleItem.indeterminate = roleCheckedMenuInfo.indeterminate;
        roleItem.checkAll = roleCheckedMenuInfo.checkAll;
        roleItem.checkedMenuIds = roleCheckedMenuInfo.menuIds;
        return roleItem;
      }

      function createRoleItems(menuTrees: TreeItem[], menuIds: Number[]) {
        const roleItems: RoleItem[] = [];
        for (const menuTree of menuTrees) {
          const roleItem: RoleItem = createRoleItem(menuTree, menuIds);
          roleItems.push(roleItem);
        }
        return roleItems;
      }

      function handldCheckGroupMenus(roleItem: RoleItem) {
        if (roleItem.checkAll) {
          roleItem.checkedMenuIds = treeToList(roleItem.treeData).map((item) => item.key);
        } else {
          roleItem.checkedMenuIds = [];
        }
        roleItem.checkNum = roleItem.checkedMenuIds.length;
        roleItem.indeterminate = roleItem.checkedMenuIds.length > 0 && roleItem.checkAll !== true;
        setCheckAllStateStatus();
      }

      function handleCheckMenus(
        checkedKeys,
        e: { checked: Boolean; checkedNodes; node; event },
        roleItem,
      ) {
        roleItem.checkedMenuIds = checkedKeys;
        roleItem.checkNum = roleItem.checkedMenuIds.length;
        roleItem.checkAll = roleItem.checkNum === treeToList(roleItem.treeData).length;
        roleItem.indeterminate = roleItem.checkedMenuIds.length > 0 && roleItem.checkAll !== true;
        setCheckAllStateStatus();
      }

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        roleId.value = data.record.id;
        const menuTree = await getMenuTreeList2({});
        const roleMenu = await getRoleMenuIds(data.record.id);
        roleItems.value = createRoleItems(menuTree, roleMenu.menuIds);
        setCheckAllStateStatus();
        setDrawerProps({ confirmLoading: false });
      });

      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
        //  emit('success', { id: unref(roleId), menuIds: checkedMenuIds });
          closeDrawer();
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }
      return {
        getTitle,
        roleItems,
        setCheckAllStateStatus,
        handldCheckGroupMenus,
        handleCheckMenus,
        handldCheckAllMenus,
        ...toRefs(checkAllState),
        handleSubmit,
        registerDrawer,
        checkeAllMenus,
      };
    },
  });
</script>

<style>
  .ant-tabs-content {
    height: 100%;
  }
</style>
