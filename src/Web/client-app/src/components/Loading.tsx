import { Spinner } from 'react-bootstrap';

const Loading = () => {
    return (
        <div className='position-relative w-100'>
            <div className='position-absolute' style={{ top: 50, left: 50}}>
                <Spinner
                    animation="border"
                    variant="danger"
                >
                    <span className="visually-hidden">Loading...</span>
                </Spinner>
            </div>
        </div>       
    );
}

export default Loading;