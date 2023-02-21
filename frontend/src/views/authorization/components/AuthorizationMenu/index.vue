<template>
  <PageWrapper>
    <Checkbox
      v-model:checked="checkAll"
      :indeterminate="indeterminate"
      @change="handldCheckAllMenus"
      v-if="checkable"
      style="margin-left: 30px; margin-bottom: 20px"
      >全选</Checkbox
    >
    <Tabs tab-position="left">
      <TabPane
        v-for="(menuItem, index) in menuItems"
        :key="`${index}`"
        :tab="`${menuItem.group}(${menuItem.checkNum})`"
      >
        <Checkbox
          v-model:checked="menuItem.checkAll"
          :indeterminate="menuItem.indeterminate"
          v-if="checkable"
          style="margin-left: 20px; margin-bottom: 10px"
          @change="handldCheckGroupMenus(menuItem)"
          >全选</Checkbox
        >
        <BasicTree
          :tree-data="menuItem.treeData"
          :checked-keys="menuItem.checkedMenuIds"
          :ref="`treeRef${index}`"
          @check="(k, e) => handleCheckMenus(k, e, menuItem)"
          defaultExpandAll
          :clickRowToExpand="false"
          :checkable="checkable"
          :selectable="false"
          multiple
        />
      </TabPane>
    </Tabs>
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, unref, reactive, toRefs, onMounted } from 'vue'
  import { BasicTree } from '/@/components/Tree/index'
  import { Checkbox, Tabs, TabPane, Tree } from 'ant-design-vue'
  import { treeToList } from '/@/utils/helper/treeHelper'
  import { MenuItem } from './typing'
  import { basicProps } from './props'
  import { PageWrapper } from '/@/components/Page'
  import { DataNode } from 'ant-design-vue/lib/tree'
  import { TreeItem } from '/@/components/Tree'
  export default defineComponent({
    name: 'AuthorizationMenu',
    components: { BasicTree, Checkbox, Tabs, TabPane, PageWrapper, Tree },
    props: basicProps,
    setup(props) {
      const menuItems = ref<MenuItem[]>([])
      const checkable = ref<Boolean>(props.checkable)
      const checkAllState = reactive({
        indeterminate: false,
        checkAll: false,
      })
      function setCheckAllStateStatus() {
        const checkAll =
          unref(menuItems).filter((item) => item.checkAll === true).length ===
          unref(menuItems).length
        const indeterminate =
          !checkAll && unref(menuItems).filter((item) => item.checkAll === true).length > 0
        checkAllState.indeterminate = indeterminate
        checkAllState.checkAll = checkAll
      }

      function handldCheckAllMenus(e: any) {
        for (const menuItem of menuItems.value) {
          if (e.target.checked) {
            menuItem.checkAll = true
            menuItem.checkedMenuIds = treeToList(menuItem.treeData).map((item) => item.key)
            menuItem.checkNum = menuItem.checkedMenuIds.length
            menuItem.indeterminate = false
          } else {
            menuItem.checkAll = false
            menuItem.checkedMenuIds = []
            menuItem.checkNum = menuItem.checkedMenuIds.length
            menuItem.indeterminate = false
          }
        }
        setCheckAllStateStatus()
      }

      function getRoleCheckedMenuInfo(
        dataNode: DataNode[] | undefined,
        menuIds: Nullable<Number[]>,
      ) {
        const roleCheckedMenuInfo: any = {}
        const dataNodeList = treeToList(dataNode)
        if (menuIds == null) {
          roleCheckedMenuInfo.checkNum = dataNodeList.length
        } else {
          if (dataNode === undefined) {
            dataNode = []
          }
          roleCheckedMenuInfo.menuIds = dataNodeList
            .filter((item) => menuIds.indexOf(item.key) >= 0)
            .map((item) => item.key)
          roleCheckedMenuInfo.checkAll = roleCheckedMenuInfo.menuIds.length === dataNodeList.length
          roleCheckedMenuInfo.indeterminate =
            roleCheckedMenuInfo.menuIds.length > 0 && roleCheckedMenuInfo.checkAll !== true
          roleCheckedMenuInfo.checkNum = roleCheckedMenuInfo.menuIds.length
        }

        return roleCheckedMenuInfo
      }

      function createMenuItem(menuTree: TreeItem, menuIds: Nullable<Number[]>) {
        let menuItem: MenuItem = {
          group: menuTree.title,
          treeData: menuTree.children,
        }
        const roleCheckedMenuInfo: any = getRoleCheckedMenuInfo(menuTree.children, menuIds)
        menuItem.checkNum = roleCheckedMenuInfo.checkNum
        menuItem.indeterminate = roleCheckedMenuInfo.indeterminate
        menuItem.checkAll = roleCheckedMenuInfo.checkAll
        menuItem.checkedMenuIds = roleCheckedMenuInfo.menuIds
        return menuItem
      }

      function createMenuItems(menuTrees: TreeItem[], menuIds: Nullable<Number[]>) {
        const menuItems: MenuItem[] = []
        for (const menuTree of menuTrees) {
          const menuItem: MenuItem = createMenuItem(menuTree, menuIds)
          menuItems.push(menuItem)
        }
        return menuItems
      }
      onMounted(() => {
        menuItems.value = createMenuItems(props.treeItems, props.menuIds)
      })

      function setMenuItems(treeItems: TreeItem[], menuIds: Nullable<Number[]>) {
        const data = createMenuItems(treeItems, menuIds)
        menuItems.value = data
        setCheckAllStateStatus()
      }

      function handldCheckGroupMenus(menuItem: MenuItem) {
        if (menuItem.checkAll) {
          menuItem.checkedMenuIds = treeToList(menuItem.treeData).map((item) => item.key)
        } else {
          menuItem.checkedMenuIds = []
        }
        menuItem.checkNum = menuItem.checkedMenuIds.length
        menuItem.indeterminate = menuItem.checkedMenuIds.length > 0 && menuItem.checkAll !== true
        setCheckAllStateStatus()
      }

      function handleCheckMenus(
        checkedKeys,
        e: { checked: Boolean; checkedNodes; node; event },
        menuItem,
      ) {
        menuItem.checkedMenuIds = checkedKeys
        menuItem.checkNum = menuItem.checkedMenuIds.length
        menuItem.checkAll = menuItem.checkNum === treeToList(menuItem.treeData).length
        menuItem.indeterminate = menuItem.checkedMenuIds.length > 0 && menuItem.checkAll !== true
        setCheckAllStateStatus()
      }

      function getCheckedAllMenuIds(): Number[] {
        let allMenuIds: Number[] = []
        unref(menuItems).forEach((item) => (allMenuIds = allMenuIds.concat(item.checkedMenuIds)))
        return allMenuIds
      }

      return {
        setCheckAllStateStatus,
        handldCheckGroupMenus,
        handleCheckMenus,
        handldCheckAllMenus,
        setMenuItems,
        getCheckedAllMenuIds,
        ...toRefs(checkAllState),
        checkable,
        menuItems,
      }
    },
  })
</script>

<style>
  .ant-tabs-content {
    height: 100%;
  }
</style>
