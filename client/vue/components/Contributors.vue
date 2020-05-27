<template>

    <ul class="contributors">
        <li v-for="contributor in todoListContributors" :key="contributor" class="contributor">
            <img
            :src="accountContributors[contributor].pictureUrl"
            :alt="accountContributors[contributor].fullName">
        </li>
    </ul>
    
</template>

<script>
    export default {
        name: "Contributors",
        props: ['todoListContributors', 'accountContributors'],
    }
</script>

<style lang="scss" scoped>

    .contributors {
        display: flex;
        list-style: none;
        padding: 0;
        margin: 0;

        .contributor {
            position: relative;

            img {
                max-width: 30px;
                border-radius: 100px;
            }
        }
    }

</style>