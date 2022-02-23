<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="40%"
    @ok="handleSubmit"
  >
    <AuthorizationMenu :checkable="true" ref="menuRef" />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { getMenuTreeList2 } from '/@/views/authorization/menu/menu.data';
  import { getRoleMenuIds } from '/@/api/role';
  import { DataNode } from 'ant-design-vue/lib/tree';
  import { AuthorizationMenu, MenuItem } from '../components/AuthorizationMenu/index';
  import { treeToList } from '/@/utils/helper/treeHelper';
  import { TreeItem } from '/@/components/Tree';
  export default defineComponent({
    name: 'RoleMenuDrawer',
    components: { BasicDrawer, AuthorizationMenu },
    setup(_, { emit }) {
      const getTitle = computed(() => '授权角色菜单');
      // const menusTreeData = ref<TreeItem[]>([]);
      const roleId = ref<Nullable<number>>();
      const menuRef = ref<{
        setMenuItems: (data: MenuItem[]) => void;
        getCheckedAllMenuIds: () => Number[];
      }>('menuRef');

      function getRoleCheckedMenuInfo(dataNode: DataNode[], menuIds: Number[]) {
        const roleCheckedMenuInfo: any = {};
        const dataNodeList = treeToList(dataNode);
        roleCheckedMenuInfo.menuIds = dataNodeList
          .filter((item) => menuIds.indexOf(item.key) >= 0)
          .map((item) => item.key);
        roleCheckedMenuInfo.checkAll = roleCheckedMenuInfo.menuIds.length === dataNodeList.length;
        roleCheckedMenuInfo.indeterminate =
          roleCheckedMenuInfo.menuIds.length > 0 && roleCheckedMenuInfo.checkAll !== true;
        roleCheckedMenuInfo.checkNum = roleCheckedMenuInfo.menuIds.length;
        return roleCheckedMenuInfo;
      }

      function createMenuItem(menuTree: TreeItem, menuIds: Number[]) {
        let menuItem: MenuItem = {
          group: menuTree.title,
          treeData: menuTree.children,
        };
        const roleCheckedMenuInfo: any = getRoleCheckedMenuInfo(menuTree.children, menuIds);
        menuItem.checkNum = roleCheckedMenuInfo.checkNum;
        menuItem.indeterminate = roleCheckedMenuInfo.indeterminate;
        menuItem.checkAll = roleCheckedMenuInfo.checkAll;
        menuItem.checkedMenuIds = roleCheckedMenuInfo.menuIds;
        return menuItem;
      }

      function createMenuItems(menuTrees: TreeItem[], menuIds: Number[]) {
        const menuItems: MenuItem[] = [];
        for (const menuTree of menuTrees) {
          const menuItem: MenuItem = createMenuItem(menuTree, menuIds);
          menuItems.push(menuItem);
        }
        return menuItems;
      }

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        roleId.value = data.record.id;
        const menuTree = await getMenuTreeList2({});
        const roleMenu = await getRoleMenuIds(data.record.id);
        const menuItems = createMenuItems(menuTree, roleMenu.menuIds);
        menuRef.value.setMenuItems(unref(menuItems));
        setDrawerProps({ confirmLoading: false });
      });

      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          const menuIds = menuRef.value.getCheckedAllMenuIds();
          emit('success', { id: unref(roleId), menuIds: menuIds });
          closeDrawer();
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }
      return {
        getTitle,
        handleSubmit,
        registerDrawer,
        menuRef,
      };
    },
  });
</script>

<style>
  .ant-tabs-content {
    height: 100%;
  }
</style>
