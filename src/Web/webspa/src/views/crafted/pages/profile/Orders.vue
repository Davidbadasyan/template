<template>
  <div>
    <!-- style="margin-top: -50px" -->
    <div class="d-flex justify-content-end">
      <button class="btn btn-sm fw-bold btn-primary mb-3" @click="redirectToAddOrder">
        Add Order
      </button>
    </div>
    <div v-if="isEmpty" class="card card-flush" :class="className">
      <!--begin::Body-->
      <div class="card-body pt-6">
        <!--begin::Table container-->
        <div class="table-responsive">
          <!--begin::Table-->
          <table class="table table-row-dashed align-middle gs-0 gy-3 my-0">
            <!--begin::Table head-->
            <thead>
              <tr class="fs-7 fw-bold text-gray-400">
                <th class="p-0 pb-3 min-w-100px text-start">
                  Number
                </th>
                <th class="pb-3 min-w-100px text-start">
                  Weight
                </th>
                <th class="pb-3 min-w-100px text-start">
                  Shipping Method
                </th>
                <th class="pb-3 min-w-100px text-start pe-12">
                  Status
                </th>
                <th class="pb-3 w-50px text-end">
                  Edit
                </th>
              </tr>
            </thead>
            <!--end::Table head-->
            <!--begin::Table body-->
            <tbody>
              <template v-for="(order, i) in tableOrders" :key="i">
                <tr>
                  <td>
                    <span class="text-gray-600 fw-bold fs-6">{{ order.number }}</span>
                  </td>
                  <td class="text-start">
                    <span class="text-gray-600 fw-bold fs-6">{{ order.weight }} {{ order.weightUnit }}</span>
                  </td>
                  <td class="text-start pe-0">
                    <span class="text-gray-600 fw-bold fs-6">{{ order.shippingMethod }}</span>
                  </td>
                  <td class="text-start pe-0 ">
                    <span class="text-gray-600 fw-bold fs-6"> {{ order.status }}</span>
                  </td>
                  <td class="text-end">
                    <button href="#" class="btn btn-sm btn-icon btn-bg-light btn-active-color-primary w-30px h-30px"
                      @click="redirectToUpdateOrder(order.id)">
                      <i class="fas fa-edit"></i>
                    </button>
                  </td>
                </tr>
              </template>
            </tbody>
            <!--end::Table body-->
          </table>
        </div>
        <!--end::Table-->
      </div>
      <!--end: Card Body-->
    </div>
    <div v-else class="card card-flush align-items-center">
      <h4>No results found, please Add order</h4>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref ,computed } from 'vue';
import { useRouter } from 'vue-router';
import { getAssetPath } from '@/core/helpers/assets';
import { useOrderStore } from '@/stores/order';

interface IOrder {
  id: number,
  number: string,
  weight: number,
  weightUnit: string,
  status: string,
  shippingMethod: string,
}

export default defineComponent({
  name: 'DefaultDashboardWidget10',
  components: {},
  props: {
    className: { type: String, required: false },
  },
  setup() {
    const router = useRouter();
    const store = useOrderStore();
    const tableOrders = ref<Array<IOrder>>([]);

    const redirectToUpdateOrder = (id: number) => {
      router.push({
        name: 'Edit Order',
        params: { orderId: id },
      });
    };

    const redirectToAddOrder = () => {
      router.push({ name: 'Add Order' });
    };
    onMounted(async () => {
      const result = await store.getOrders()
      if (result) {
        tableOrders.value = [...result.data]
      }
    })
    const isEmpty = computed(() => {
      return tableOrders.value.length
    });
    return {
      tableOrders,
      getAssetPath,
      redirectToUpdateOrder,
      redirectToAddOrder,
      isEmpty,
    };
  },
});
</script>
