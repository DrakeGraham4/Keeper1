
<template>
  <Modal id="active-keep">
    <template #modal-body>
      <div class="container">
        <div class="row">
          <div class="col-md-6 p-0">
            <img
              class="w-100 object-fit-cover img-fluid"
              :title="keep.name"
              :src="keep.img"
              alt=""
            />
          </div>
          <div class="col-md-6">
            <div class="row mt-2 pb-4">
              <div class="d-flex align-items-center justify-content-around">
                <h4>
                  <i
                    title="Total Views"
                    style="color: purple"
                    class="mdi mdi-eye-plus p-2"
                    >{{ keep.views }}</i
                  >
                  <i
                    title="Times Kept"
                    style="color: purple"
                    class="mdi mdi-inbox-arrow-down p-2"
                    >{{ keep.kept }}</i
                  >
                </h4>
              </div>
            </div>

            <div class="row text-center">
              <div class="pb-5">
                <h4>
                  {{ keep.name }}
                </h4>
              </div>
            </div>
            <div class="row">
              <div class="pt-5 pb-5 mb-5 text-wrap">
                <p class="text-break">{{ keep.description }}</p>
              </div>
            </div>
            <div class="row justify-content-between align-items-center">
              <div class="col-md-3">
                <img
                  @click="goToProfile(keep.creator.id)"
                  class="rounded-circle selectable profile-img col-3"
                  :src="keep.creator?.picture"
                  alt=""
                />
              </div>
              <div class="col-md-3">
                <div class="d-flex">
                  <div class="ps-1">
                    <p>
                      {{ keep.creator?.name }}
                    </p>
                  </div>
                  <div>
                    <i
                      title="Delete Keep"
                      v-if="keep.creatorId == account.id"
                      @click="deleteKeep(keep.id)"
                      class="selectable mdi mdi-trash-can col-3"
                    ></i>
                  </div>
                </div>
              </div>

              <div class="col-md-3" v-if="account.id">
                <button
                  class="btn dropdown-toggle"
                  type="button"
                  data-bs-toggle="dropdown"
                  title="Add to Vault"
                  aria-expanded="false"
                >
                  Add to a Vault
                </button>
                <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                  <li
                    class="selectable"
                    @click="createVaultKeep(v.id)"
                    v-for="v in vaults"
                    :key="v.id"
                  >
                    {{ v.name }}
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
  <link
    rel="stylesheet"
    href="//cdn.materialdesignicons.com/5.4.55/css/materialdesignicons.min.css"
  />
</template>


<script>
import { computed } from '@vue/reactivity'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
import { router } from '../router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
export default {
  setup() {
    const router = useRouter()
    return {
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      profileVaults: computed(() => AppState.profileVaults),
      vaults: computed(() => AppState.vaults),

      goToProfile(id) {
        Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
        router.push({ name: 'Profile', params: { id } })
      },

      async createVaultKeep(vaultId) {

        let data = {
          keepId: AppState.activeKeep.id,
          vaultId: vaultId
        }
        await vaultsService.createVaultKeep(data)
        Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
      },

      async deleteKeep(keepId) {
        logger.log(keepId)
        try {
          await keepsService.deleteKeep(keepId)
          Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message)
        }
      }

    }
  }
}
</script>

<style lang="scss" scoped>
.profile-img {
  width: 50px;
  height: 50px;
}
</style>