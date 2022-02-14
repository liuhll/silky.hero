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
          <BasicTree :tree-data="menusTreeData" ref="menuTreeRef" />
        </Col>
        <Col :span="12">
          <div>数据权限: {{ dataRangeText }}</div>
          <List
            v-if="showCustomOrganizationDataRanges"
            :data-source="roleDetail.customOrganizationDataRanges"
            bordered
            style="margin-top: 10px"
          >
            <template #renderItem="{ item }">
              <ListItem>{{ item.name }}</ListItem>
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
  import { treeMap } from '/@/utils/helper/treeHelper';
  import { getRoleDetailById } from '/@/api/role';

  export default defineComponent({
    name: 'RoleDetailDrawer',
    components: { BasicDrawer, Card, Description, Row, Col, BasicTree, List, ListItem },
    setup() {
      const getTitle = ref('角色');
      const roleDetail = ref();
      const menuTreeRef = ref<Nullable<TreeActionType>>(null);
      const menusTreeData = ref<TreeItem[]>([]);
      const dataRangeText = ref<string>();
      const showCustomOrganizationDataRanges = ref<boolean>(false);

      function getMenuTree() {
        const tree = unref(menuTreeRef);
        if (!tree) {
          throw new Error('tree is null!');
        }
        return tree;
      }

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

      function setMenusTreeData(menus: any[]) {
        menusTreeData.value = treeMap(menus, {
          conversion: (node: any) => {
            return {
              title: node.title,
              key: node.menuId,
              value: node.menuId,
            };
          },
        });
      }

      const [registerDrawer] = useDrawerInner(async (id) => {
        const data = await getRoleDetailById(id);
        getTitle.value = data.realName;
        roleDetail.value = data;
        setMenusTreeData(data.menus);
        setDataRange(data.dataRange);
      });
      return {
        getTitle,
        roleDetail,
        roleDetailSchemas,
        menuTreeRef,
        menusTreeData,
        dataRangeText,
        showCustomOrganizationDataRanges,
        registerDrawer,
      };
    },
  });
</script>
