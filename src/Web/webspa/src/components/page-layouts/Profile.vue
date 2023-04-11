<template>
  <!--begin::Navbar-->
  <div>
    <div>
      <!--begin::Details-->
      <div class="d-flex flex-wrap flex-sm-nowrap mb-3">
        <!--begin: Pic-->
        <!-- <div class="me-7 mb-4">
          <div
            class="symbol symbol-100px symbol-lg-160px symbol-fixed position-relative"
          >
            <img :src="getAssetPath('media/avatars/300-1.jpg')" alt="image" />
            <div
              class="position-absolute translate-middle bottom-0 start-100 mb-6 bg-success rounded-circle border border-4 border-white h-20px w-20px"
            ></div>
          </div>
        </div> -->
        <!--end::Pic-->

        <!--begin::Info-->
        <div class="flex-grow-1">
          <!--begin::Title-->
          <!--end::Title-->

          <!--begin::Stats-->
          <div class="d-flex flex-wrap flex-stack">
            <!--begin::Wrapper-->
            <div class="d-flex flex-column flex-grow-1 pe-8">
              <!--begin::Stats-->
              <div class="d-flex flex-wrap">
                <!--begin::Stat-->
                <!--end::Stat-->

                <!--begin::Stat-->
                <!--end::Stat-->

                <!--begin::Stat-->
                <!--end::Stat-->
              </div>
              <!--end::Stats-->
            </div>
            <!--end::Wrapper-->

            <!--begin::Progress-->
            <!-- <div
              class="d-flex align-items-center w-200px w-sm-300px flex-column mt-3"
            >
              <div class="d-flex justify-content-between w-100 mt-auto mb-2">
                <span class="fw-semobold fs-6 text-gray-400"
                  >Profile Compleation</span
                >
                <span class="fw-bold fs-6">50%</span>
              </div>

              <div class="h-5px mx-3 w-100 bg-light mb-3">
                <div
                  class="bg-success rounded h-5px"
                  role="progressbar"
                  style="width: 50%"
                  aria-valuenow="50"
                  aria-valuemin="0"
                  aria-valuemax="100"
                ></div>
              </div>
            </div> -->
            <!--end::Progress-->
          </div>
          <!--end::Stats-->
        </div>
        <!--end::Info-->
      </div>
      <!--end::Details-->

      <!--begin::Navs-->
      <!-- <div class="d-flex overflow-auto h-55px"> -->
        <ul
          class="nav nav-stretch nav-line-tabs nav-line-tabs-2x border-transparent fs-5 fw-bold flex-nowrap"
        >
          <!--begin::Nav item-->
          <!-- <li class="nav-item">
            <router-link
              to="/crafted/pages/profile/overview"
              class="nav-link text-active-primary me-6"
              active-class="active"
            >
              Overview
            </router-link>
          </li> -->
          <!--end::Nav item-->
          <!--begin::Nav item-->
          <!-- <li class="nav-item">
            <router-link
              class="nav-link text-active-primary me-6"
              to="/crafted/pages/profile/projects"
              active-class="active"
            >
              Projects
            </router-link>
          </li> -->
          <!--end::Nav item-->
          <!--begin::Nav item-->
          <!-- <li class="nav-item">
            <router-link
              class="nav-link text-active-primary me-6"
              to="/crafted/pages/profile/campaigns"
              active-class="active"
            >
              Campaigns
            </router-link>
          </li> -->
          <!--end::Nav item-->
          <!--begin::Nav item-->
          <!-- <li class="nav-item">
            <router-link
              class="nav-link text-active-primary me-6"
              to="/crafted/pages/profile/documents"
              active-class="active"
            >
              Documents
            </router-link>
          </li> -->
          <!--end::Nav item-->
          <!--begin::Nav item-->
          <!-- <li class="nav-item">
            <router-link
              class="nav-link text-active-primary me-6"
              to="/crafted/pages/profile/connections"
              active-class="active"
            >
              Connections
            </router-link>
          </li> -->
          <!--end::Nav item-->
          <!--begin::Nav item-->
          <!-- <li class="nav-item">
            <router-link
              class="nav-link text-active-primary me-6"
              to="/crafted/pages/profile/activity"
              active-class="active"
            >
              Activity
            </router-link>
          </li> -->
          <!-- <li class="nav-item">
            <router-link
              class="nav-link text-active-primary me-6"
              to="/crafted/pages/profile/orders"
              active-class="active"
            >
              Orders
            </router-link>
          </li> -->
          <!--end::Nav item-->
        </ul>
      <!-- </div> -->
      <!--begin::Navs-->
    </div>
  </div>
  <!--end::Navbar-->
  <router-view></router-view>
</template>

<script lang="ts">
import { getAssetPath } from "@/core/helpers/assets";
import { defineComponent } from "vue";
import Dropdown3 from "@/components/dropdown/Dropdown3.vue";

export default defineComponent({
  name: "profile-page-layout",
  components: {
    Dropdown3,
  },
  setup() {
    return {
      getAssetPath,
    };
  },
});
</script>
