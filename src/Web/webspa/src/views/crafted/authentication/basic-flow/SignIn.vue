<template>
  <div class="w-lg-500px p-10">
    <VForm class="form w-100" id="kt_login_signin_form" @submit="onSubmitLogin" :validation-schema="login"
      :initial-values="{ email: 'geghmayan@g.com', password: 'Aa!12345' }">
      <div class="text-center mb-10">
        <h1 class="text-dark mb-3">Sign In</h1>
        <div class="text-gray-400 fw-semobold fs-4">
          New Here?

          <router-link to="/sign-up" class="link-primary fw-bold">
            Create an Account
          </router-link>
        </div>
      </div>
      <div class="fv-row mb-10">
        <label class="form-label fs-6 fw-bold text-dark">Email</label>
        <Field tabindex="1" class="form-control form-control-lg form-control-solid" type="text" name="email"
          autocomplete="off" />
        <div class="fv-plugins-message-container">
          <div class="fv-help-block">
            <ErrorMessage name="email" />
          </div>
        </div>
      </div>
      <div class="fv-row mb-10">
        <div class="d-flex flex-stack mb-2">
          <label class="form-label fw-bold text-dark fs-6 mb-0">Password</label>
          <!-- <router-link to="/password-reset" class="link-primary fs-6 fw-bold">
            Forgot Password ?
          </router-link> -->
        </div>
        <Field tabindex="2" class="form-control form-control-lg form-control-solid" type="password" name="password"
          autocomplete="off" />
        <div class="fv-plugins-message-container">
          <div class="fv-help-block">
            <ErrorMessage name="password" />
          </div>
        </div>
      </div>
      <div class="text-center">
        <button tabindex="3" type="submit" ref="submitButton" id="kt_sign_in_submit"
          class="btn btn-lg btn-primary w-100 mb-5">
          <span class="indicator-label"> Continue </span>

          <span class="indicator-progress">
            Please wait...
            <span class="spinner-border spinner-border-sm align-middle ms-2"></span>
          </span>
        </button>
      </div>
    </VForm>
  </div>
</template>

<script lang="ts">
import { getAssetPath } from "@/core/helpers/assets";
import { defineComponent, ref } from "vue";
import { ErrorMessage, Field, Form as VForm } from "vee-validate";
import { useAuthStore, type User } from "@/stores/auth";
import { useRouter } from "vue-router";
import Swal from "sweetalert2/dist/sweetalert2.js";
import * as Yup from "yup";
import JwtService from "@/core/services/JwtService";

export default defineComponent({
  name: "sign-in",
  components: {
    Field,
    VForm,
    ErrorMessage,
  },
  setup() {
    const store = useAuthStore();
    const router = useRouter();

    const submitButton = ref<HTMLButtonElement | null>(null);

    //Create form validation object
    const login = Yup.object().shape({
      email: Yup.string().email().required().label("Email"),
      password: Yup.string().min(4).required().label("Password"),
    });

    //Form submit function
    const onSubmitLogin = async (values: any) => {
      values = values as User;
      // Clear existing errors
      store.logout();

      if (submitButton.value) {
        // eslint-disable-next-line
        submitButton.value!.disabled = true;
        // Activate indicator
        submitButton.value.setAttribute("data-kt-indicator", "on");
      }

      // Send login request
      await store.login(values);
      // const error = Object.values(store.errors);
      const token = JwtService.getToken()
      if (token != null) {
        router.push({ name: "dashboard" });
      } else {
        Swal.fire({
          text: 'login failed',
          icon: "error",
          buttonsStyling: false,
          confirmButtonText: "Try again!",
          heightAuto: false,
          customClass: {
            confirmButton: "btn fw-semobold btn-light-danger",
          },
        }).then(() => {
          store.errors = {};
        });
      }

      //Deactivate indicator
      submitButton.value?.removeAttribute("data-kt-indicator");
      // eslint-disable-next-line
      submitButton.value!.disabled = false;
    };

    return {
      onSubmitLogin,
      login,
      submitButton,
      getAssetPath,
    };
  },
});
</script>
