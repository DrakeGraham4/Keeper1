<template>
    <Modal id="active-keep">
        <template #modal-body> 
            <div class="container">
                <div class="row">
                    <div class="col-md-6 p-0">
                        <img class="w-100 object-fit-cover img-fluid" :src="keep.img" alt=""/>
                    </div>
                    <div class="col-md-6 p-2">
                        <div class="mt-3 d-flex">
                            {{keep.name}}
                            {{keep.description}}
                            <img @click="goToProfile(keep.creator.id)" class="rounded-circle selectable" :src="keep.creator?.picture" alt=""/>
                            
                            </div>
                        </div>
                    </div>
                </div>
            
        </template>
    </Modal>
    
</template>


<script>
import { computed } from '@vue/reactivity'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'
import { router } from '../router'
export default {
    setup(){
        const router = useRouter()
        return {
            keep: computed(() => AppState.activeKeep),
            account: computed(() => AppState.account),
            profile: computed(()=> AppState.profile),
            goToProfile(id){
                Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
                router.push({name: 'Profile', params: {id}})
            }
            
           
        }
    }
}
</script>


<style lang="scss" scoped>

</style>