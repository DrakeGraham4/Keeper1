<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 d-flex p-5">
        <h3 class="pe-5">{{ activeVault.name }}</h3>
        <h5 class="px-5 pt-1">Keeps: {{ vaultKeeps.length }}</h5>
        <div class="px-5">
          <button
            v-if="activeVault.creatorId == account.id"
            @click="deleteVault(activeVault.id)"
            class="btn btn-danger"
          >
            Delete Vault
          </button>
        </div>
      </div>
    </div>
    <div class="masonry-with-columns">
      <div v-for="k in vaultKeeps" :key="k.id">
        <Keep :keep="k" />
      </div>
      <!-- {{vaultKeeps}} -->
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import Keep from '../components/Keep.vue'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { profilesService } from '../services/ProfilesService'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import { router } from '../router'
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.getKeepsByVaultId(route.params.id)
        await vaultsService.getById(route.params.id)
      } catch (error) {
        router.push({ name: 'Home' })
        logger.error(error)
        Pop.toast('Private Vault!')
      }
    })
    return {
      activeVault: computed(() => AppState.activeVault),
      vault: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      vaultKeeps: computed(() => AppState.vaultKeeps),

      async deleteVault(vaultId) {
        try {
          logger.log(vaultId)
          await vaultsService.deleteVault(vaultId)
          router.push({ name: 'Profile', params: { id: AppState.account.id } })
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
</style>