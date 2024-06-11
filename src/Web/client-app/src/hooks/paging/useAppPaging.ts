import { useState } from "react"

interface IPage {
    page: number
}

const defaultPaging: IPage = {
    page: 1
}

const useAppPaging = (
    updateMainPagingState?: (page: number) => void
) => {
    const [ hasPaging, setHasPaging ] = useState(defaultPaging);

    const handlePaging = (page: number) => {
        setHasPaging({
            ...hasPaging,
            page
        });

        if (updateMainPagingState)
            updateMainPagingState(page);
    }

    return {
        hasPaging,
        handlePaging
    }
}

export default useAppPaging;