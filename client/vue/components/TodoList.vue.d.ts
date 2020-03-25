declare const _default: {
    name: string;
    props: string[];
    data(): {
        todos: {
            id: number;
            name: string;
        }[];
    };
    methods: {
        showItemDetails(): void;
    };
    components: {
        TodoItem: {
            name: string;
            props: string[];
            data(): {};
            methods: {
                showItemDetails(): void;
            };
        };
    };
};
export default _default;
