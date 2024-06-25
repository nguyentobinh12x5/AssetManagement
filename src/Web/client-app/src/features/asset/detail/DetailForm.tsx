import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import ConfirmModal from "../../../components/confirmModal/ConfirmModal";
import ButtonIcon from "../../../components/ButtonIcon";
import { XCircle } from "react-bootstrap-icons";
import { getAssetById } from "../reducers/asset-detail-slice";
import Table from "react-bootstrap/Table";
import { RootState } from "../../../redux/store";
import { Col, Row } from "react-bootstrap";

const DetailForm = ({ id }: { id: number }) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const dispatch = useDispatch();
  const { assetDetail } = useSelector((state: RootState) => state.assetDetail);

  useEffect(() => {
    if (isModalOpen) {
      dispatch(getAssetById(id));
    }
  }, [isModalOpen, dispatch, id]);

  const handleShowModal = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
  };

  return (
    <div className="container m-auto p-5">
      <ButtonIcon onClick={handleShowModal}>
        <XCircle color="red" />
      </ButtonIcon>

      <ConfirmModal
        title="Detailed Asset Information"
        isShow={isModalOpen}
        onHide={handleCloseModal}
        dialogClassName="modal-150w modal-detail"
        isShowClose={true}
      >
        <div className="form-detail">
          <div>
            <Row className="mb-3">
              <Col md={3}>Asset Code</Col>
              <Col md={9}>
                {assetDetail?.code}
                123213
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Asset Name</Col>
              <Col md={9}>
                {assetDetail?.name}
                21321312
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Category</Col>
              <Col md={9}>
                {assetDetail?.categoryName}
                3123123
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Installed Date</Col>
              <Col md={9}>A123</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>State</Col>
              <Col md={9}>A123</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Location</Col>
              <Col md={9}>A123</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>Specification</Col>
              <Col md={9}>A123</Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>History</Col>
              <Col md={9}>
                <Table>
                  <thead>
                    <tr className="table-detail">
                      <th>Date</th>
                      <th>Assigned to</th>
                      <th>Assigned by</th>
                      <th>Returned Date</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr className="table-row-detail">
                      <td>12/10/2018</td>
                      <td>hungtv1</td>
                      <td>binhnv</td>
                      <td>07/03/2019</td>
                    </tr>
                    <tr className="table-row-detail">
                      <td>10/03/2024</td>
                      <td>thinhptx</td>
                      <td>tuanha</td>
                      <td>22/03/2024</td>
                    </tr>
                  </tbody>
                </Table>
              </Col>
            </Row>
          </div>
        </div>
      </ConfirmModal>
    </div>
  );
};

export default DetailForm;
