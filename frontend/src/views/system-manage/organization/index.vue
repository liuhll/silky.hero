<template>
  <PageWrapper>
    <Row :gutter="[24, 16]">
      <Col :span="6">
        <Card title="组织机构树">
          <BasicTree
            defaultExpandAll
            :treeData="treeData"
            :beforeRightClick="getRightMenuList"
            ref="treeRef"
          />
        </Card>
      </Col>
      <Col :span="18"> <Card title="成员" class="w-3/4 xl:w-4/5"></Card></Col>
    </Row>
  </PageWrapper>
</template>
<script lang="ts">
import { PageWrapper } from '/@/components/Page';
import { Card, Row, Col } from 'ant-design-vue';
import { BasicTree, TreeActionType, TreeItem, ContextMenuItem } from '/@/components/Tree';
import { defineComponent, ref, unref, onMounted } from 'vue';
import { getOrganizationTree } from '/@/api/organization';
import { treeMap } from '/@/utils/helper/treeHelper';
import { GetOrgizationTreeModel } from '/@/api/organization/model/organizationModel';

export default defineComponent({
  components: { Card, Row, Col, PageWrapper, BasicTree },
  setup() {
    const treeRef = ref<Nullable<TreeActionType>>(null);
    const treeData = ref<TreeItem[]>([]);
    function getTree() {
      const tree = unref(treeRef);
      if (!tree) {
        throw new Error('tree is null!');
      }
      return tree;
    }

    onMounted(async () => {
      await loadOrganizationTreeData();
      getTree().expandAll(true);
    });

    async function loadOrganizationTreeData() {
      const organizationTreeList = await getOrganizationTree();
      treeData.value = treeMap(organizationTreeList, {
        conversion: (node: GetOrgizationTreeModel) => {
          return {
            title: node.name,
            key: node.id,
            icon: 'ant-design:folder-outlined',
          };
        },
      });
    }
    function getRightMenuList(node: any): ContextMenuItem[] {
      return [
        {
          label: '新增',
          handler: () => {
            console.log('点击了新增', node);
          },
          icon: 'bi:plus',
        },
        {
          label: '删除',
          handler: () => {
            console.log('点击了删除', node);
          },
          icon: 'ant-design:delete-outlined',
        },
      ];
    }
    return {
      treeData,
      treeRef,
      getRightMenuList,
    };
  },
});
</script>
