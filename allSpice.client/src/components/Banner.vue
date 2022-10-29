<template>
    <div class="col-11 p-0 mt-3 position-relative">
        <img
          src="https://plus.unsplash.com/premium_photo-1663126351065-741a1d338b5d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2832&q=80"
          alt=""
          class="img img-fluid elevation-1 rounded"
        />
        <div class="row">
          <div class="col-8 bg-white position-absolute overlay rounded d-flex justify-content-between elevation-5">
            <button class="btn btn-primary">button</button>
            <button class="btn btn-primary">button</button>
            <button class="btn btn-primary">button</button>
            <button class="btn btn-primary">button</button>
          </div>
        </div>
      </div>
</template>


<script>
export default {
  setup(){
    return {}
  }
}
</script>


<style lang="scss" scoped>
.img {
  width: 100%;
  height: 25vh;
  object-fit: cover;
}

.overlay{
  bottom: -20px;
  right: 150px;
}
</style>