<template>
    <div class="component">
        <div class="card" style="width: 18rem;">
  <div class="card-body">
    <h5 class="card-title">{{profileVaults.name}}</h5>
  </div>
</div>

    </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
export default {
    setup(){
        return {
            profileVaults: computed(() => AppState.vaults)
        }
        
    }
}
</script>


<style lang="scss" scoped>

</style>