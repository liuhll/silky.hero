<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="30%"
    @ok="handleSubmit"
  >
    <Checkbox :indeterminate="indeterminate" v-model:checked="checkAll" @change="checkeAllMenus">全选</Checkbox>
    <BasicTree
      :tree-data="menusTreeData"
      defaultExpandAll
      checkable
      multiple
      ref="treeRef"
      @change="handleCheckedMenus"
    />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref, reactive, toRefs } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { BasicTree, TreeActionType } from '/@/components/Tree/index';
  import { TreeItem } from '/@/components/Tree';
  import { Checkbox } from 'ant-design-vue';
  import { treeToList } from '/@/utils/helper/treeHelper';
  import { arrayEquals } from '/@/utils';
  export default defineComponent({
    name: 'RoleMenuDrawer',
    components: { BasicDrawer, BasicTree, Checkbox },
    setup(_, { emit }) {
      const getTitle = computed(() => '授权角色菜单');
      const menusTreeData = ref<TreeItem[]>([]);
      const treeRef = ref<Nullable<TreeActionType>>(null);
      const roleId = ref<Nullable<number>>();

      const checkAllState = reactive({
        indeterminate: false,
        checkAll: false,
      });

      function getTree() {
        const tree = unref(treeRef);
        if (!tree) {
          throw new Error('tree is null!');
        }
        return tree;
      }

      function setCheckAllStateStatus(indeterminate: boolean, checkAll: boolean) {
        checkAllState.indeterminate = indeterminate;
        checkAllState.checkAll = checkAll;
      }

      function checkeAllMenus(e: any) {
        checkAllState.indeterminate = false;
        getTree().checkAll(e.target.checked);
      }

      function setMenusTreeData(treeData: TreeItem[]) {
        menusTreeData.value = treeData;
      }

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        roleId.value = data.record.id;
        setDrawerProps({ confirmLoading: false });
      });

      function handleCheckedMenus(keys: number[]) {
        const allMenuIds = treeToList(unref(menusTreeData))
          .map((item) => item.key)
          .sort((item1, item2) => item1 - item2);
        const isCheckedAll = arrayEquals(
          allMenuIds,
          keys.sort((item1, item2) => item1 - item2),
        );
        checkAllState.checkAll = isCheckedAll;
        checkAllState.indeterminate = !isCheckedAll && keys.length > 0;
      }

      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          const checkedMenuIds = getTree().getCheckedKeys();
          emit('success', { id: unref(roleId), menuIds: checkedMenuIds });
          closeDrawer();
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }
      return {
        getTitle,
        menusTreeData,
        treeRef,
        setCheckAllStateStatus,
        ...toRefs(checkAllState),
        handleSubmit,
        registerDrawer,
        getTree,
        setMenusTreeData,
        checkeAllMenus,
        handleCheckedMenus,
      };
    },
  });
</script>
