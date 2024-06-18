import React, { useState } from 'react';
import { APP_CONFIG } from '../../../constants/appConfig';
import ConfirmModal from '../../../components/confirmModal/ConfirmModal';

const ConfirmDisable = () => {
    const [isModalOpen, setIsModalOpen] = useState(false);

    const handleShowModal = () => {
        setIsModalOpen(true);
    };

    const handleCloseModal = () => {
        setIsModalOpen(false);
    };

    const handleDisableUser = () => {
        setIsModalOpen(false)
    }

    return (
        <div className="container m-auto p-5">

            <button onClick={handleShowModal}>Open Modal</button>

            <ConfirmModal
                title="Are you sure?"
                isShow={isModalOpen}
                onHide={handleCloseModal}
                // dialogClassName="custom-modal"
            >
                <div className="modal-body-content">
                    <p>Do you want to disable this user?</p>
                    <div className="modal-buttons">
                        <button className="btn btn-danger" onClick={handleDisableUser}>Disable</button>
                        <button className="btn btn-light btn-outline-secondary" onClick={handleCloseModal}>Cancel</button>
                    </div>
                </div>
                
            </ConfirmModal>
        </div>
    );
};

export default ConfirmDisable;