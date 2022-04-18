<template>
    <div class="component container">
        <div class="row mt-4">
            <div class="col-12 d-flex">
                <img :src="profile.picture" alt=""/>
                <div class="ms-3 d-flex flex-column justify-content-center">
                    <h1>{{profile.name}}</h1>
                    {{profileVaults}}
                </div>
            </div>
        </div>
        <!-- <div class="masonry-with-columns">
            <div v-for="p in profileVaults" :key="p.id" >
                
            </div>
        </div> -->


    </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
import {profilesService} from '../services/ProfilesService.js'
import {vaultsService} from '../services/VaultsService.js'
export default {
    setup(){
        const route = useRoute()
        onMounted(async () => {
            try {
                await profilesService.getProfile(route.params.id)
                await profilesService.getVaults(route.params.id)
                
            } catch (error) {
                logger.error(error)
                Pop.toast(error.message, 'error')
            }
        })
        return {
            profile: computed(() => AppState.profile),
            keeps: computed(() => AppState.keeps),
            profileVaults: computed(() => AppState.profileVaults)
        }
    }
}
</script>


<style lang="scss" scoped>

</style>