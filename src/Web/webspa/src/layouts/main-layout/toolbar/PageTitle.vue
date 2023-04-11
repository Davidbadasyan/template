<template>
  <!--begin::Page title-->
  <div
    v-if="pageTitleDisplay"
    :class="`page-title d-flex flex-${pageTitleDirection} justify-content-center flex-wrap me-3`"
  >
    <template v-if="pageTitle">
      <h1
        class="page-heading d-flex text-dark fw-bold fs-3 flex-column justify-content-center my-0"
      >
        {{ pageTitle }}
      </h1>
    </template>
  </div>
  <div v-else class="align-items-stretch"></div>
  <!--end::Page title-->
</template>

<script lang="ts">
import { computed, defineComponent } from "vue";
import {
  pageTitleBreadcrumbDisplay,
  pageTitleDirection,
  pageTitleDisplay,
} from "@/core/helpers/config";
import { useRoute } from "vue-router";

export default defineComponent({
  name: "layout-page-title",
  components: {},
  setup() {
    const route = useRoute();

    const pageTitle = computed(() => {
      return route.meta.pageTitle;
    });

    const breadcrumbs = computed(() => {
      return route.meta.breadcrumbs;
    });

    return {
      pageTitle,
      breadcrumbs,
      pageTitleDisplay,
      pageTitleBreadcrumbDisplay,
      pageTitleDirection,
    };
  },
});
</script>
