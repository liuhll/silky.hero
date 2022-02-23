<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="40%"
    destroyOnClose
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
  import { AuthorizationMenu } from '../components/AuthorizationMenu/index';
  import { TreeItem } from '/@/components/Tree';

  export default defineComponent({
    name: 'RoleMenuDrawer',
    components: { BasicDrawer, AuthorizationMenu },
    setup(_, { emit }) {
      const getTitle = computed(() => '授权角色菜单');
      const roleId = ref<Nullable<number>>();
      const menuRef = ref<{
        setMenuItems: (data: TreeItem[], menuIds: Nullable<number[]>) => void;
        getCheckedAllMenuIds: () => Number[];
      }>('menuRef');

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        roleId.value = data.record.id;
        const menuTrees = await getMenuTreeList2({});
        const roleMenu = await getRoleMenuIds(data.record.id);
        menuRef.value.setMenuItems(menuTrees, roleMenu.menuIds);
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
