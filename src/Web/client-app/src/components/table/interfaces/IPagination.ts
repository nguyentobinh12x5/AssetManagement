export default interface IPagination {
    currentPage?: number;
    totalPage?: number;
    handleChange: (page: number) => void;
}